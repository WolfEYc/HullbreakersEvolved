using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [CreateAssetMenu(order = 1, fileName = "newWave", menuName = "Waves/Wave")]
    public class Wave : SerializedScriptableObject
    {
        public List<Encounter> encounters;

        public float ttkwave;
    }
}
