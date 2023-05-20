using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [CreateAssetMenu(order = 2, fileName = "newWaveList", menuName = "Waves/WaveList")]
    public class WaveList : SerializedScriptableObject
    {
        public List<Wave> waves;
    }
}
