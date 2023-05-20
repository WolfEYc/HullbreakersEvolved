using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public class CountBuff : MultiplierEffectInt
    {
        public override void Do(GameObject context)
        { 
            context.GetComponentInChildren<WeaponMods>().count.AddToHierarchy(buff);
        }

        public override void Undo(GameObject context)
        {
            context.GetComponentInChildren<WeaponMods>().count.RemoveFromHierarchy(buff);
        }
    }
}
