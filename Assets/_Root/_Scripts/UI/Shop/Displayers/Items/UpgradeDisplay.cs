using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class UpgradeDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text upgradeName;
        [SerializeField] Image upgradeImage;
        [SerializeField] GameObject enableOnNoUpgrade;
        
        public void AssignUpgrade(Upgrade upgrade)
        {
            bool nullUpgrade = upgrade == null;
            
            enableOnNoUpgrade.SetActive(nullUpgrade);
            gameObject.SetActive(!nullUpgrade);
            
            if(nullUpgrade) return;

            upgradeName.SetText(upgrade.name);
            upgradeImage.sprite = upgrade.icon;
        }
    }
}
