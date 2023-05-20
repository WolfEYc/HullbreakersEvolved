using System;
using UnityEngine;

namespace Hullbreakers
{
    public class PlayerAbilities : MonoBehaviour, IPlayerControllable
    {
        [field:SerializeField] public Ability active { get; private set; }
        [field:SerializeField] public Ability ultimate { get; private set; }
        [field:SerializeField] public Ability passive { get; private set; }

        public enum AbilityEnum
        {
            Passive,
            Active,
            Ultimate
        }

        public Ability GetAbility(AbilityEnum abilityEnum)
        {
            switch (abilityEnum)
            {
                case AbilityEnum.Passive:
                    return passive;
                case AbilityEnum.Active:
                    return active;
                case AbilityEnum.Ultimate:
                    return ultimate;
                default:
                    throw new ArgumentOutOfRangeException(nameof(abilityEnum), abilityEnum, null);
            }
        }


        public void SetPlayerControl()
        {
            PlayerManager.instance.playerInput.ActiveAbilityUsed += active.Use;
            PlayerManager.instance.playerInput.UltimateAbilityUsed += ultimate.Use;
            
        }

        public void RemovePlayerControl()
        {
            PlayerManager.instance.playerInput.ActiveAbilityUsed -= active.Use;
            PlayerManager.instance.playerInput.UltimateAbilityUsed -= ultimate.Use;
        }
    }
}
