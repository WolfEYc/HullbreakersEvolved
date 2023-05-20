using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(UIButton))]
    public class PurchaseButton : MonoBehaviour
    {
        [SerializeField] PurchaseMenu purchaseMenu;

        UIButton _purchaseButton;
        
        void Awake()
        {
            _purchaseButton = GetComponent<UIButton>();
            _purchaseButton.onClickEvent.AddListener(purchaseMenu.PurchaseItem);
            purchaseMenu.ItemSelected += RefreshButton;
            purchaseMenu.ItemPurchased += _ => RefreshButton();
        }

        void RefreshButton()
        {
            _purchaseButton.interactable = purchaseMenu.selectedDisplay.affordable && !purchaseMenu.selectedDisplay.so.purchased;
        }
    }
}
