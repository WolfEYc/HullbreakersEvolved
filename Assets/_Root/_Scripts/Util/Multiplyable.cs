using System;
using System.Collections.Generic;
using UnityEngine;


namespace Hullbreakers
{
    [Serializable]
    public class Multiplyable
    {
        public event Action OnValueChanged;
        
        [SerializeField] float baseValue;
        [SerializeField] float multiplier;

        readonly List<Multiplyable> _localHeirarchy = new();
        
        public float localValue { get; private set; }
        public float value { get; private set; }

        float GetValue()
        {
            localValue = BaseValue * Multiplier;
            
            float tempbaseValue = baseValue;
            float tempmultiplier = multiplier;

            foreach (Multiplyable multiplyable in _localHeirarchy)
            {
                tempbaseValue += multiplyable.baseValue;
                tempmultiplier += multiplyable.multiplier;
            }

            
            return tempbaseValue * tempmultiplier;
        }

        public void RefreshValue()
        {
            value = GetValue();
            OnValueChanged?.Invoke();
        }

        public void AddToHierarchy(Multiplyable multiplyable)
        {
            _localHeirarchy.Add(multiplyable);
            multiplyable.OnValueChanged += RefreshValue;
            RefreshValue();
        }

        public void RemoveFromHierarchy(Multiplyable multiplyable)
        {
            _localHeirarchy.Remove(multiplyable);
            multiplyable.OnValueChanged -= RefreshValue;
            RefreshValue();
        }
        
        public float Multiplier
        {
            get => multiplier;
            set
            {
                multiplier = value;
                RefreshValue();
            }
        }
        
        public float BaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;
                RefreshValue();
            }
        }

        public Multiplyable(Multiplyable multiplyable)
        {
            baseValue = multiplyable.BaseValue;
            multiplier = multiplyable.multiplier;
        }
        
        public Multiplyable(float newBaseValue)
        {
            Reset(newBaseValue);
        }
        
        public Multiplyable(float newbaseValue, float newmultiplier)
        {
            baseValue = newbaseValue;
            multiplier = newmultiplier;
        }

        public Multiplyable()
        {
            baseValue = 0f;
            multiplier = 1f;
        }
        

        public void Reset(float newBaseValue)
        {
            BaseValue = newBaseValue;
            Multiplier = 1f;
        }
    }
    
    [Serializable]
    public class MultiplyableInt
    {
        public event Action OnValueChanged;
        
        [SerializeField] int baseValue;
        [SerializeField] float multiplier;

        readonly List<MultiplyableInt> _localHeirarchy = new();
        
        public int localValue { get; private set; }
        public int value { get; private set; }

        int GetValue()
        {
            localValue = (int)(BaseValue * Multiplier);
            
            float tempbaseValue = baseValue;
            float tempmultiplier = multiplier;

            foreach (MultiplyableInt multiplyable in _localHeirarchy)
            {
                tempbaseValue += multiplyable.baseValue;
                tempmultiplier += multiplyable.multiplier;
            }
            
            return (int)(tempbaseValue * tempmultiplier);
        }

        public void RefreshValue()
        {
            value = GetValue();
            OnValueChanged?.Invoke();
        }

        public void AddToHierarchy(MultiplyableInt multiplyable)
        {
            _localHeirarchy.Add(multiplyable);
            multiplyable.OnValueChanged += RefreshValue;
            RefreshValue();
        }

        public void RemoveFromHierarchy(MultiplyableInt multiplyable)
        {
            _localHeirarchy.Remove(multiplyable);
            multiplyable.OnValueChanged -= RefreshValue;
            RefreshValue();
        }
        
        public float Multiplier
        {
            get => multiplier;
            set
            {
                multiplier = value;
                RefreshValue();
            }
        }
        
        public int BaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;
                RefreshValue();
            }
        }

        public MultiplyableInt(MultiplyableInt multiplyable)
        {
            baseValue = multiplyable.BaseValue;
            multiplier = multiplyable.multiplier;
        }
        
        public MultiplyableInt(int newBaseValue)
        {
            Reset(newBaseValue);
        }
        
        public MultiplyableInt(int newbaseValue, float newmultiplier)
        {
            baseValue = newbaseValue;
            multiplier = newmultiplier;
        }

        public MultiplyableInt()
        {
            baseValue = 0;
            multiplier = 1f;
        }
        
        public void Reset(int newBaseValue)
        {
            BaseValue = newBaseValue;
            Multiplier = 1f;
        }
    }
}
