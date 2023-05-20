using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hullbreakers
{
    public class ItemsDisplay : MonoBehaviour
    {
        [SerializeField] ItemDisplay itemDisplayPrefab;
        [SerializeField] UnitsSo defaultUnitsToDisplay;
        [SerializeField] PurchaseMenu purchaseMenu;

        [SerializeField] int capacity = -1;
        
        [field:SerializeField] public bool oneTimePurchases { get; private set; }

        readonly List<ItemDisplay> _itemDisplays = new();
        UnitSo[] _unitsPool;
        

        public event Action<ItemDisplay> UnitSelected;
        public event Action UnitDeselected;

        public UnitSo this [int index]
        {
            get => _itemDisplays[index].so;
            set => _itemDisplays[index].so = value;
        }
        
        public void AddDisplay(UnitSo so)
        {
            ItemDisplay itemDisplay = Instantiate(itemDisplayPrefab, transform);
            itemDisplay.currencyProvider = purchaseMenu.currencyProvider;
            itemDisplay.so = so;
            
            itemDisplay.Selected += UnitSelected;
            itemDisplay.Deseleted += UnitDeselected;
            
            _itemDisplays.Add(itemDisplay);
        }

        public void RemoveDisplay(int index)
        {
            ItemDisplay itemDisplay = _itemDisplays[index];
            _itemDisplays.RemoveAt(index);
            Destroy(itemDisplay);
        }

        public void RefreshAffordable()
        {
            foreach (ItemDisplay itemDisplay in _itemDisplays)
            {
                itemDisplay.RefreshAffordable();
            }
        }

        void Awake()
        {
            _unitsPool = new UnitSo[defaultUnitsToDisplay.units.Length];
            Array.Copy(defaultUnitsToDisplay.units, _unitsPool, _unitsPool.Length);
        }

        void Start()
        {
            for (int i = 0; i != capacity && i != defaultUnitsToDisplay.units.Length; i++)
            {
                AddDisplay(defaultUnitsToDisplay.units[i]);
            }
        }

        public void ShuffleDisplay()
        {
            _unitsPool.Shuffle();
            for(int i = 0; i < capacity; i++)
            {
                this[i] = _unitsPool[i];
            }
        }
    }
}
