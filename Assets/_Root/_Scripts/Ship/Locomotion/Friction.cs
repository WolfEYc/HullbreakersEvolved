using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PhysicsMods))]
    public class Friction : MonoBehaviour
    {
        PhysicsMods _physicsMods;
        
        Rigidbody2D _rb;
        LocalTimeScale _localTimeScale;
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _localTimeScale = GetComponentInParent<LocalTimeScale>();
            _physicsMods = GetComponent<PhysicsMods>();
        }
        void FixedUpdate()
        {
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, _physicsMods.friction.value * Time.fixedDeltaTime * _localTimeScale.timeScale.value);
        }

        public void ToggleOn()
        {
            enabled = true;
        }

        public void ToggleOff()
        {
            enabled = false;
        }
    }
}
