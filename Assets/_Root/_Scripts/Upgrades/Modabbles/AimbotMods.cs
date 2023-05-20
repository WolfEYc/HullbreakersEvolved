using UnityEngine;

namespace Hullbreakers
{
    public class AimbotMods : MonoBehaviour
    {
        public Multiplyable maxvel;
        public Multiplyable accel;

        void Awake()
        {
            maxvel.RefreshValue();
            accel.RefreshValue();
        }
    }
}
