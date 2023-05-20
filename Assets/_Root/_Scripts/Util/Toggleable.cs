using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public class Toggleable
    {
        public event Action TurnedOn, TurnedOff;

        [SerializeField] bool _state;
        public bool state
        {
            get => _state;
            set
            {
                if(_state == value) return;
                _state = value;

                if (_state)
                {
                    TurnedOn?.Invoke();
                }
                else
                {
                    TurnedOff?.Invoke();
                }
            }
        }
    }
}
