using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public sealed class WeaponDamageBuff : MultiplierEffect
    {
        public override void Do(GameObject context)
        {
            context.GetComponentInChildren<WeaponMods>().damage.AddToHierarchy(buff);
        }

        public override void Undo(GameObject context)
        {
            context.GetComponentInChildren<WeaponMods>().damage.RemoveFromHierarchy(buff);
        }
    }
}
