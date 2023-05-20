using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class OnHitDamager : MonoBehaviour, IActionOnDestroyed
    {
        IDamager[] _damagers;
        Rigidbody2D _rb;
        bool _effective;
        

        void Awake()
        {
            _damagers = GetComponentsInChildren<IDamager>();
            _rb = GetComponent<Rigidbody2D>();
        }

        void OnEnable()
        {
            _effective = true;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!_effective || !col.TryGetComponent(out IDamageable damageable)) return;

            foreach (IDamager damager in _damagers)
            {
                damager.InflictDamage(damageable, col.ClosestPoint(_rb.position), _rb.velocity);   
            }
        }
        
        public void OnDestroyed()
        {
            _effective = false;
        }
    }
}
