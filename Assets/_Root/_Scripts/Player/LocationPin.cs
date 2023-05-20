using UnityEngine;


namespace Hullbreakers
{
    public class LocationPin : MonoBehaviour
    {
        [SerializeField] Rigidbody2D parentRb;
        [SerializeField] Rotation rotation;
        

        public void Pin(Vector2 position)
        {
            parentRb.velocity = Vector2.zero;
            parentRb.isKinematic = true;
            parentRb.position = position;
            parentRb.rotation = 90f;
            rotation.enabled = false;
        }

        
        public void Unpin()
        {
            parentRb.isKinematic = false;
            rotation.enabled = true;
            PlayerManager.instance.playerShipControl.SetPlayerControl();
        }
    }
}
