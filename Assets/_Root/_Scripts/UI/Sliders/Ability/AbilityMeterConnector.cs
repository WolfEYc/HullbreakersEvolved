using System;
using UnityEngine;

namespace Hullbreakers
{
    public class AbilityMeterConnector : Singleton<AbilityMeterConnector>
    {
        [SerializeField] AbilityMeter passive, active, ultimate;

        void Start()
        {
            PlayerManager.instance.OnNewShip += OnNewShip;
        }

        void OnNewShip()
        {
            passive.ability = PlayerManager.instance.playerAbilities.passive;
            active.ability = PlayerManager.instance.playerAbilities.active;
            ultimate.ability = PlayerManager.instance.playerAbilities.ultimate;
        }

        public AbilityMeter GetAbilityMeter(PlayerAbilities.AbilityEnum abilityEnum)
        {
            switch (abilityEnum)
            {
                case PlayerAbilities.AbilityEnum.Passive:
                    return passive;
                case PlayerAbilities.AbilityEnum.Active:
                    return active;
                case PlayerAbilities.AbilityEnum.Ultimate:
                    return ultimate;
                default:
                    throw new ArgumentOutOfRangeException(nameof(abilityEnum), abilityEnum, null);
            }
        }
    }
}
