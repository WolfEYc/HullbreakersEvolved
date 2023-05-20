using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class KillSphere : MonoBehaviour, IDamager
    {
        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out IDamageable damageable)) return;
            var pos = _rb.position;
            var closestPt = col.ClosestPoint(pos);
            InflictDamage(damageable, closestPt, closestPt - pos);
        }

        public int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir)
        {
            return (int)damageable.Damage(999999f, Vector2.zero);
        }
    }
}
