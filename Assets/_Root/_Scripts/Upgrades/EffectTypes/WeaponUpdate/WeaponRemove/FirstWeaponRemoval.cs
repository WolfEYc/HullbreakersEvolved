using UnityEngine;

namespace Hullbreakers
{
    public class FirstWeaponRemoval : ModEffect
    {
        public override void Do(GameObject context)
        {
            Object.Destroy(context.GetComponentInChildren<Weapon>().GameObject);
        }

        public override void Undo(GameObject context)
        {
            
        }
    }
}
