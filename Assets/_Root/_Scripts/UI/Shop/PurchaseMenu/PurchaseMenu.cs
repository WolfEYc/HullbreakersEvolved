using System;
using UnityEngine;

namespace Hullbreakers
{
    public abstract class PurchaseMenu : MonoBehaviour
    {
        [field: SerializeField] public ItemsDisplay itemsDisplay { get; private set; }
        [field: SerializeField] public ItemDisplay selectedDisplay { get; private set; }
        
        protected ItemDisplay ReferencedItemFromShop;

        public event Action<UnitSo> ItemPurchased;
        public event Action ItemSelected;
        
        public abstract ICurrencyProvider currencyProvider { get; }

        protected virtual void Awake()
        {
            itemsDisplay.UnitSelected += OnUnitSelected;
            currencyProvider.currency.CashChanged += itemsDisplay.RefreshAffordable;
            selectedDisplay.currencyProvider = currencyProvider;
        }

        void OnUnitSelected(ItemDisplay item)
        {
            ReferencedItemFromShop = item;
            selectedDisplay.so = item.so;
            ItemSelected?.Invoke();
        }

        public void PurchaseItem()
        {
            if (!selectedDisplay.affordable)
            {
                throw new InvalidOperationException("Cannot Afford!");
            }

            selectedDisplay.Purchase();

            if (itemsDisplay.oneTimePurchases)
            {
                if (selectedDisplay.so.purchased)
                {
                    throw new InvalidOperationException("Already Purchased!");
                }

                selectedDisplay.so.purchased = true;
            }
            
            selectedDisplay.RefreshDisplay();
            ReferencedItemFromShop.RefreshDisplay();
            
            ItemPurchased?.Invoke(selectedDisplay.so);
        }
    }
}
