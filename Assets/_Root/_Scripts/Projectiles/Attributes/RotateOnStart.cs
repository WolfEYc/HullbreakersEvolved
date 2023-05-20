using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotateOnStart : MonoBehaviour
    {
        [SerializeField] float angularVel;
        void Start()
        {
            GetComponent<Rigidbody2D>().angularVelocity = angularVel;
        }
    }
}
