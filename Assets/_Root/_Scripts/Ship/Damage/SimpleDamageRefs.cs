using UnityEngine;

namespace Hullbreakers
{
    public class SimpleDamageRefs : MonoBehaviour
    {
        [field: SerializeField] public Collider2D hitBox { get; private set; }
        [field: SerializeField] public Health hp { get; private set; }
        [field:SerializeField] public Health shield { get; private set; }

        void OnEnable()
        {
            AimbotSystems.instance.AddHealth(hp);
        }
    }
}
