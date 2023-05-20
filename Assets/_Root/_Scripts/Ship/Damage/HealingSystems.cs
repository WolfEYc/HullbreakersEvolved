using UnityEngine;

namespace Hullbreakers
{
    public class HealingSystems : Singleton<HealingSystems>
    {
        [field: SerializeField] public HealingSystem playerHeals { get; private set; }
        [field: SerializeField] public HealingSystem enemyHeals { get; private set; }
    }
}
