using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DroneJoint : MonoBehaviour
    {
        [Tooltip("0 = No movement/rotation, 1 = instant teleportation")]
        [SerializeField] float moveForce;
        [SerializeField] float rotForce;

        Vector3 _fixedPos;
        Quaternion _fixedRot;
        
        Transform _target, _self;
        Rigidbody2D _selfRb;
        
        float rotationToTarget => Mathf.DeltaAngle(_selfRb.rotation, _target.eulerAngles.z);
        Vector2 vectorToTarget => (Vector2)_target.position - _selfRb.position;

        void Awake()
        {
            _self = transform;
            _target = new GameObject(name).transform;
            _target.parent = PlayerManager.instance.droneTargets;
            _selfRb = GetComponent<Rigidbody2D>();
        }
        public void RefreshOffsets()
        {
            _target.SetPositionAndRotation(_self.position, _self.rotation);
            _fixedPos = _target.position;
            _fixedRot = _target.rotation;
            _selfRb.isKinematic = false;
        }
        public void TeleportToTarget()
        {
            _self.SetPositionAndRotation(_fixedPos, _fixedRot);
            _selfRb.velocity = Vector2.zero;
            _selfRb.angularVelocity = 0f;
            _selfRb.isKinematic = true;
        }
        public void PhysicsUpdate()
        {
            _selfRb.velocity = moveForce * vectorToTarget;
            _selfRb.angularVelocity = rotForce * rotationToTarget;
        }
    }
}
