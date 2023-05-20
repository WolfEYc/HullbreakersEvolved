using UnityEngine;

namespace Hullbreakers
{
    public abstract class RaycastWeapon : Weapon, IDamager
    {
        protected Vector2 Endpos;
        
        [SerializeField] IDamageFeedback[] _damageFeedbacks;
        
        ContactFilter2D _contactFilter;
        RaycastHit2D[] _results;

        protected override void Awake()
        {
            base.Awake();
            _contactFilter.useLayerMask = true;
            _contactFilter.layerMask = Physics2D.GetLayerCollisionMask(GameObject.layer);

            mods.pierce.OnValueChanged += ReallocateResults;

            ReallocateResults();
        }

        
        void ReallocateResults()
        {
            _results = new RaycastHit2D[mods.pierce.value + 1];
        }

        public override void Shoot()
        {
            Vector2 right = Transform.right;
            float size = mods.size.value;
            
            int hits = Physics2D.Raycast(
                Transform.position, 
                right,
                _contactFilter,
                _results,
                size
            );
            
            for (int i = 0; i < hits; i++)
            {
                if(!_results[i].collider.TryGetComponent(out IDamageable damageable)) return;
                
                InflictDamage(damageable, _results[i].point, right);
            }
            
            Endpos.x = hits != _results.Length ? size : Vector2.Distance(_results[^1].point,Transform.position);
        }
        
        public virtual int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir)
        {
            int resultdmg = (int)damageable.Damage(mods.damage.value, dir * mods.knockback.value);
            
            for(int i = 0 ; i < _damageFeedbacks.Length; i++)
            {
                _damageFeedbacks[i].DeployFeedback(resultdmg, pos, dir);
            }
            
            OnInflicDmg?.Invoke(damageable, resultdmg);

            return resultdmg;
        }
    }
}
