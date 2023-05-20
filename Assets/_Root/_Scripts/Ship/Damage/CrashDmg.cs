using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PhysicsMods))]
    public class CrashDmg : SerializedMonoBehaviour, IDamager
    {
        Rigidbody2D _rb;
        PhysicsMods _physicsMods;

        [SerializeField] IDamageFeedback[] _crashDmgFeedback;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _physicsMods = GetComponent<PhysicsMods>();
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if(!col.gameObject.TryGetComponent(out IDamageable damageable)) return;
            
            InflictDamage(damageable, col.GetContact(0).point, _rb.velocity);
        }

        public int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir)
        {
            int dmg = (int)damageable.CrashDamage(
                _rb.velocity.sqrMagnitude
                * _physicsMods.crashDmg.Multiplier
                + _physicsMods.crashDmg.value);

            for(int i = 0; i < _crashDmgFeedback.Length; i++)
            {
                _crashDmgFeedback[i].DeployFeedback(dmg, pos, dir);
            }

            return dmg;
        }
        
        
    }
}
