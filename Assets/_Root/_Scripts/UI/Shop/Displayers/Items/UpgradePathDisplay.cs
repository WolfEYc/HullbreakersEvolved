using System;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    public class UpgradePathDisplay : MonoBehaviour
    {
        [SerializeField] Upgradeable.UpgradePath path;
        [SerializeField] UIButton upgradebutton;

        [SerializeField] UpgradeDisplay current, next;
        
        [SerializeField] TMP_Text upgradeDescription;
        [SerializeField] TMP_Text upgradePrice;

        [SerializeField] UpgradeMeter upgradeMeter;

        [SerializeField] GameObject pathDisplay, lockedDisplay;

        Upgradeable _upgradeable;
        Upgrade _currentUpgrade, _nextUpgrade;

        public event Action OnPurchased;

        void Awake()
        {
            PlayerOrbs.instance.currency.CashChanged += UpdateAffordable;
            upgradebutton.onClickEvent.AddListener(Purchased);
        }

        void UpdateAffordable()
        {
            upgradebutton.interactable = _nextUpgrade != null && _upgradeable.CanAffordUpgrade(_nextUpgrade);
            upgradePrice.color = _nextUpgrade != null && _upgradeable.CanAffordUpgrade(_nextUpgrade)
                ? Color.white
                : Color.red;
        }

        public void AssignUpgradeable(Upgradeable upgradeable)
        {
            _upgradeable = upgradeable;
            _currentUpgrade = upgradeable.GetCurrentUpgradeOnPath(path);
            _nextUpgrade = upgradeable.GetNextUpgradeOnPath(path);
            
            upgradeMeter.FillMeter(upgradeable.TierOnPath(path));
            AssignButtonsAndImages();
            UpdateAffordable();
            UpdatePathDisplay();
        }

        void UpdatePathDisplay()
        {
            bool locked = _upgradeable.PathLocked(path);
            
            pathDisplay.SetActive(!locked);
            lockedDisplay.SetActive(locked);
        }
        
        void AssignButtonsAndImages()
        {
            next.AssignUpgrade(_nextUpgrade);
            current.AssignUpgrade(_currentUpgrade);
            
            upgradeDescription.SetText(_nextUpgrade == null ? "MAX UPGRADES" : _nextUpgrade.description);

            if (_nextUpgrade != null)
            {
                upgradePrice.SetText(_nextUpgrade.price.ToString());
            }
        }

        void Purchased()
        {
            _upgradeable.PurchaseUpgrade(path);
            OnPurchased?.Invoke();
        }
    }
}
