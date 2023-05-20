using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Knockback : MonoBehaviour
    {
        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void ApplyKnockback(Vector2 kbForce)
        {
            _rb.AddForce(kbForce, ForceMode2D.Impulse);
        }
    }
}
