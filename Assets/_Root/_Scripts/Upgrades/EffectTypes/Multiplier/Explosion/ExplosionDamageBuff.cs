using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public sealed class ExplosionDamageBuff : MultiplierEffect
    {
        public override void Do(GameObject context)
        {
            context.GetComponentInChildren<ExplosionMods>().explosionDamage.AddToHierarchy(buff);
        }

        public override void Undo(GameObject context)
        {
            context.GetComponentInChildren<ExplosionMods>().explosionDamage.RemoveFromHierarchy(buff);
        }
    }
}
