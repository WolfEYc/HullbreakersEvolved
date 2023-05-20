using UnityEngine;

namespace Hullbreakers
{
    public class ZoneWeapon : Weapon, IDamager
    {
        [SerializeField] float radius;
        
        ContactFilter2D _contactFilter;
        public Collider2D[] results { get; private set; }
        public int hits { get; private set; }
        
        [SerializeField] IDamageFeedback[] _DamageFeedbacks;
        
        protected override void Awake()
        {
            base.Awake();
            _contactFilter.useLayerMask = true;
            _contactFilter.layerMask = Physics2D.GetLayerCollisionMask(GameObject.layer);

            mods.count.OnValueChanged += ReallocateResults;
            
            OnStoppedShooting += SetHitsZero;

            ReallocateResults();
        }

        void SetHitsZero()
        {
            hits = 0;
        }

        protected virtual void ReallocateResults()
        {
            results = new Collider2D[mods.count.value];
        }

        public override void Shoot()
        {
            Vector2 pos = Transform.position;
            
            hits = Physics2D.OverlapCircle(pos, radius, _contactFilter, results);

            for (int i = 0; i < hits; i++)
            {
                if(!results[i].TryGetComponent(out IDamageable damageable)) return;

                var closestPt = results[i].ClosestPoint(pos);

                InflictDamage(damageable, closestPt, closestPt - pos);
            }
        }

        public virtual int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir)
        {
            int effectResult;
            float damage = mods.damage.value;

            effectResult = (int)damageable.Damage(damage, dir * mods.knockback.value);
            
            for(int i = 0; i < _DamageFeedbacks.Length; i++)
            {
                _DamageFeedbacks[i].DeployFeedback(effectResult, pos, dir);
            }

            OnInflicDmg?.Invoke(damageable, effectResult);
            
            return effectResult;
        }
    }
}
