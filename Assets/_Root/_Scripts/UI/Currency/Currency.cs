using System;
using UnityEngine;


namespace Hullbreakers
{
    public class Currency
    {
        readonly bool _persistent;
        readonly string _name;
        
        int _cash;
        public event Action CashChanged;
        public int cash
        {
            get => _cash;
            set
            {
                _cash = value;

                if (_cash < 0)
                {
                    throw new InvalidOperationException("Cant go in debt dummy");
                }

                if (_persistent)
                {
                    PlayerPrefs.SetInt(_name, _cash);
                }

                CashChanged?.Invoke();
            }
        }

        public Currency(string name, bool persistent)
        {
            _persistent = persistent;
            _name = name;
            if (_persistent)
            {
                cash = PlayerPrefs.GetInt(_name, 0);
            }
        }
        
        
    }
}
