using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hullbreakers
{
    [Serializable]
    public class AddWeapon : ModEffect
    {
        [SerializeField] Weapon weapon;
        
        public override void Do(GameObject context)
        {
            Object.Instantiate(weapon, context.GetComponentInChildren<WeaponHolder>().transform);
            context.GetComponent<ColorController>().RefreshColorables();
        }

        public override void Undo(GameObject context)
        {
            
        }
    }
}
