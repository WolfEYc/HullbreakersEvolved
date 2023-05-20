using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public sealed class FirerateBuff : MultiplierEffect
    {
        public override void Do(GameObject context)
        { 
            context.GetComponentInChildren<WeaponMods>().shotsPerMinute.AddToHierarchy(buff);
        }

        public override void Undo(GameObject context)
        {
            context.GetComponentInChildren<WeaponMods>().shotsPerMinute.RemoveFromHierarchy(buff);
        }
    }
}
