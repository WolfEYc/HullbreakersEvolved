using System;
using UnityEngine;

namespace Hullbreakers
{
    public class AbilityMods : MonoBehaviour
    {
        [field: SerializeField] public Multiplyable coolDownTime { get; private set; }
        [field: SerializeField] public Multiplyable duration { get; private set; }

        void Awake()
        {
            coolDownTime.RefreshValue();
            duration.RefreshValue();
        }
    }
}
