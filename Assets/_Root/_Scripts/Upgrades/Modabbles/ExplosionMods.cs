using UnityEngine;

namespace Hullbreakers
{
    public class ExplosionMods : MonoBehaviour
    {
        public Multiplyable explosionRadius;
        public Multiplyable explosionDamage;
        

        void Awake()
        {
            explosionDamage.RefreshValue();
            explosionRadius.RefreshValue();
        }
    }
}
