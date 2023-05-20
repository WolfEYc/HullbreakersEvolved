using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public sealed class BlastRadiusBuff : MultiplierEffect
    {
        public override void Do(GameObject context)
        { 
            context.GetComponentInChildren<ExplosionMods>().explosionRadius.AddToHierarchy(buff);
        }

        public override void Undo(GameObject context)
        {
            context.GetComponentInChildren<ExplosionMods>().explosionRadius.RemoveFromHierarchy(buff);
        }
    }
}
