using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

namespace Hullbreakers
{
    public class UpgradableDisplay : MonoBehaviour
    {
        [SerializeField] ItemDisplay unitDisplay;
        [SerializeField] UIContainer selfContainer;
        [SerializeField] UIButton sellBtn;
        
        [SerializeField] UpgradePathDisplay[] upgradesDisplay;
        

        Drone _drone;
        Upgradeable _upgradeable;

        void Awake()
        {
            SelectionManager.instance.OnSelected += OnSelected;
            unitDisplay.currencyProvider = PlayerOrbs.instance;
            foreach (UpgradePathDisplay upgradePathDisplay in upgradesDisplay)
            {
                upgradePathDisplay.OnPurchased += RefreshDisplay;
            }
        }

        void OnSelected()
        {
            if(!((Component)SelectionManager.instance.Selected).TryGetComponent(out Upgradeable upgradeable)) return;
            _upgradeable = upgradeable;
            selfContainer.Show();
            RefreshDisplay();
            DisplayDrone();
        }

        void DisplayDrone()
        {
            if(!_upgradeable.TryGetComponent(out Drone drone))
            {
                sellBtn.gameObject.SetActive(false);
                return;
            }

            _drone = drone;
            sellBtn.gameObject.SetActive(true);
        }


        void RefreshDisplay()
        {
            unitDisplay.so = _upgradeable.selfUnit;
            foreach (UpgradePathDisplay upgradeDisplay in upgradesDisplay)
            {
                upgradeDisplay.AssignUpgradeable(_upgradeable);
            }
        }

        public void SellDrone()
        {
            _drone.Sell();
            selfContainer.Hide();
            SelectionManager.instance.Selected = null;
        }
    }
}
