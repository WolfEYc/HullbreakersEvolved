using UnityEngine;

namespace Hullbreakers
{
    public class FirerateAbility : Ability
    {
        public Multiplyable buff;
        
        [SerializeField] WeaponMods weaponMods;
        
        protected override void Awake()
        {
            base.Awake();
            Used += OnUsed;
            Ended += OnEnded;
        }

        void OnUsed()
        {
            weaponMods.shotsPerMinute.AddToHierarchy(buff);
        }
        
        void OnEnded()
        {
            weaponMods.shotsPerMinute.RemoveFromHierarchy(buff);
        }
    }
}
