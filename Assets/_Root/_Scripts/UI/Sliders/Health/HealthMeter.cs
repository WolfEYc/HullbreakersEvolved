using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(ResponsiveSizeMeter))]
    public class HealthMeter : MonoBehaviour
    {
        ResponsiveSizeMeter _responsiveSizeMeter;
        Health _health;
        
        void Awake()
        {
            _responsiveSizeMeter = GetComponent<ResponsiveSizeMeter>();
        }

        public void AssignHealth(Health health)
        {
            _health = health;
            _health.OnDamaged += OnHealthChanged;
            _health.OnHealed += OnHealthChanged;
            _health.healthMods.maxHp.OnValueChanged += OnMaxHealthChanged;
            OnMaxHealthChanged();
            OnHealthChanged(69f);
        }
        
        void OnHealthChanged(float obj)
        {
            _responsiveSizeMeter.SetCurrentValue(_health.hp);
        }

        void OnMaxHealthChanged()
        {
            _responsiveSizeMeter.SetMaxValue(_health.healthMods.maxHp.value);
        }
    }
}
