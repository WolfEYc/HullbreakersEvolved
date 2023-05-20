using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    public class RerollsDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        [SerializeField] UIButton uiButton;
        
        void Awake()
        {
            PlayerRerolls.instance.currency.CashChanged += Refresh;
            Refresh();
        }
        
        void Refresh()
        {
            text.SetText(PlayerRerolls.instance.currency.cash.ToString());
            uiButton.interactable = PlayerRerolls.instance.currency.cash > 0;
        }
    }
}
