using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public abstract class MultiplierEffect : ModEffect
    {
        [field:SerializeField] public Multiplyable buff { get; private set; }
    }
    
    [Serializable]
    public abstract class MultiplierEffectInt : ModEffect
    {
        [field:SerializeField] public MultiplyableInt buff { get; private set; }
    }
}
