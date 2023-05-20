using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

namespace Hullbreakers
{
    public class DronePurchaseMenu : PurchaseMenu
    {
        [SerializeField] UIContainer container;

        public override ICurrencyProvider currencyProvider => PlayerOrbs.instance;

        protected override void Awake()
        {
            base.Awake();
            ItemSelected += OnItemSelected;
            itemsDisplay.UnitDeselected += OnUnitDeselected;
        }

        void OnUnitDeselected()
        {
            container.Hide();
        }

        void OnItemSelected()
        {
            container.Show();
        }
        
        
    }
}
