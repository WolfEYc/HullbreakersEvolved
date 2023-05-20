using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [CreateAssetMenu(order = 4, fileName = "newUnitsGroup", menuName = "Shipyard")]
    public class UnitsSo : SerializedScriptableObject
    {
        [field: SerializeField] public UnitSo[] units { get; private set; }

    }
}
