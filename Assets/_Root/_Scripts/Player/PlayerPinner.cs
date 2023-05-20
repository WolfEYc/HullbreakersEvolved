using UnityEngine;

namespace Hullbreakers
{
    public class PlayerPinner : MonoBehaviour
    {
        public void Pin()
        {
            PlayerManager.instance.PinPlayer(transform.position);
        }

        public void UnPin()
        {
            PlayerManager.instance.UnPinPlayer();
        }
    }
}
