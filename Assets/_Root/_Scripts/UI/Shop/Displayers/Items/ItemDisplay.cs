using System;
using Doozy.Runtime.UIManager.Animators;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class ItemDisplay : MonoBehaviour, ISelectable
    {
        public event Action<ItemDisplay> Selected;
        public event Action Deseleted;

        const string k_PurchasedText = "PURCHASED";

        public bool affordable { get; private set; }

        public ICurrencyProvider currencyProvider { get; set; }

        UnitSo _so;
        public UnitSo so
        {
            get => _so;
            set
            {
                _so = value;
                RefreshDisplay();
            }
        }

        [SerializeField] Image itemImage, bottomBar;
        [SerializeField] TMP_Text itemName, itemPrice, itemDescription;
        [SerializeField] UISelectableColorAnimator bg, icon;
        
        public void RefreshDisplay()
        {
            RefreshAffordable();
            
            if (itemImage != null)
            {
                itemImage.sprite = _so.sprite;
                itemImage.color = _so.color;

                if (icon != null)
                {
                    icon.SetStartColor(_so.color);
                }
            }

            if (bottomBar != null)
            {
                Color color = _so.color;
                color.a = bottomBar.color.a;
                bottomBar.color = color;
                
                if (bg != null)
                {
                    bg.SetStartColor(color);
                }
            }
            
            if (itemName != null)
            {
                itemName.SetText(_so.name);
            }

            if (itemPrice != null)
            {
                itemPrice.SetText(so.purchased ? k_PurchasedText : _so.price.ToString());
                
            }

            if (itemDescription != null)
            {
                itemDescription.SetText(_so.description);
            }
        }

        public void RefreshAffordable()
        {
            affordable = so.price <= currencyProvider.currency.cash;
            if (itemPrice != null)
            {
                itemPrice.color = affordable ? Color.white : Color.red;
            }
        }

        public void InvokeSelect()
        {
            SelectionManager.instance.Select(this);
        }

        public void Select()
        {
            Selected?.Invoke(this);
        }
        
        public void Deselect()
        {
            Deseleted?.Invoke();
        }

        public void Purchase()
        {
            currencyProvider.currency.cash -= so.price;
        }

        public void Refund()
        {
            currencyProvider.currency.cash += so.price;
        }
    }
}
