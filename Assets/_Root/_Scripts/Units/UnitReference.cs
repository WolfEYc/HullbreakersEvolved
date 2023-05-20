using UnityEngine;

namespace Hullbreakers
{
    public class UnitReference : MonoBehaviour
    {
        [field: SerializeField] public UnitSo selfUnit { get; private set; }
    }
}
