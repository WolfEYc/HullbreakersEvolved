using UnityEngine;

namespace Hullbreakers
{
    public class HealthMeterConnector : Singleton<HealthMeterConnector>
    {
        [SerializeField] HealthMeter shieldMeter, hpMeter;

        void Start()
        {
            PlayerManager.instance.OnNewShip += OnNewShip;
        }

        void OnNewShip()
        {
            shieldMeter.AssignHealth(PlayerManager.instance.playerHealth.shield);
            hpMeter.AssignHealth(PlayerManager.instance.playerHealth.hp);
        }
    }
}
