using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Hullbreakers
{
    public class ShipPurchaseMenu : PurchaseMenu
    {
        public override ICurrencyProvider currencyProvider => PlayerCash.instance;

        [SerializeField] UIButton equipBtn;

        protected override void Awake()
        {
            base.Awake();
            equipBtn.onClickEvent.AddListener(SetPlayerPrefab);
            ItemSelected += RefreshEquip;
            ItemPurchased += _ => RefreshEquip();
        }

        void RefreshEquip()
        {
            equipBtn.interactable = selectedDisplay.so.purchased && PlayerManager.instance.playerPrefab != selectedDisplay.so.prefab;
        }

        void SetPlayerPrefab()
        {
            PlayerManager.instance.playerPrefab = selectedDisplay.so.prefab;
            RefreshEquip();
        }
    }
}
