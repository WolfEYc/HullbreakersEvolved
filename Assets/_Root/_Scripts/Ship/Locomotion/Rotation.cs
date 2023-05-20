using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PhysicsMods))]
    public class Rotation : MonoBehaviour, IPlayerControllable
    {
        public Vector2 target;
        Rigidbody2D _rb;

        public Multiplyable rotationSpeed { get; private set; }

        LocalTimeScale _localTimeScale;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _localTimeScale = GetComponentInParent<LocalTimeScale>();
            rotationSpeed = GetComponent<PhysicsMods>().rotationSpeed;
        }
        
        void FixedUpdate()
        {
            _rb.MoveRotation(Mathf.MoveTowardsAngle(
                _rb.rotation,
                (target - _rb.position).AngleFromDirection(),
            rotationSpeed.value * Time.fixedDeltaTime * _localTimeScale.timeScale.value
            ));
        }
        
        void OnPlayerTargetUpdated()
        {
            target = PlayerManager.instance.playerInput.target;
        }
        
        public void SetPlayerControl()
        {
            PlayerManager.instance.playerInput.TargetUpdated += OnPlayerTargetUpdated;
        }

        public void RemovePlayerControl()
        {
            PlayerManager.instance.playerInput.TargetUpdated -= OnPlayerTargetUpdated;
        }
    }
}
