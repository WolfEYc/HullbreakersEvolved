using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hullbreakers
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AbilityMeter : MonoBehaviour, IColorable
    {
        [SerializeField] Slider slider;
        [field: SerializeField] public Image fill { get; private set; }
        [field: SerializeField] public Image icon { get; private set; }
        [SerializeField] Material rainbowMat;
        [SerializeField] UnityEvent used;
        [SerializeField] UnityEvent durationEnded;
        [SerializeField] UnityEvent offCooldown;

        CanvasGroup _cg;
        Timer _activeTimer;

        Action _updateSlider;
        
        Ability _ability;
        public Ability ability
        {
            get => _ability;
            set
            {
                if (_ability != null)
                {
                    UnBindToAbility();
                }

                _ability = value;
                BindToAbility();
                SetActive(true);
            }
        }

        void Awake()
        {
            _cg = GetComponent<CanvasGroup>();
            offCooldown.AddListener(OffCooldown);
            enabled = false;
        }

        public void SetActive(bool active)
        {
            _cg.alpha = active ? 1f : 0f;
        }

        void BindToAbility()
        {
            _ability.Used += AbilityOnUsed;
            _ability.Used += used.Invoke;
            
            _ability.Ended += AbilityOnEnded;
            _ability.Ended += durationEnded.Invoke;
            
            _ability.OffCd += AbilityOffCd;
            _ability.OffCd += offCooldown.Invoke;
            icon.sprite = _ability.image;
        }

        void AbilityOffCd()
        {
            enabled = false;
            _activeTimer = null;
            slider.value = 1f;
        }

        void AbilityOnEnded()
        {
            _activeTimer = _ability.countDownTimer;
            _updateSlider = UpdateCooldown;
        }

        void AbilityOnUsed()
        {
            _activeTimer = _ability.durationTimer;
            _updateSlider = UpdateDuration;
            enabled = true;
        }

        void UnBindToAbility()
        {
            _ability.Used -= AbilityOnUsed;
            _ability.Used -= used.Invoke;
            
            _ability.Ended -= AbilityOnEnded;
            _ability.Ended -= durationEnded.Invoke;
            
            _ability.OffCd -= AbilityOffCd;
            _ability.OffCd -= offCooldown.Invoke;
        }
        
        void FixedUpdate()
        {
            _updateSlider();
        }

        void UpdateDuration()
        {
            slider.value = (_activeTimer.endTime - Time.time) /
                           _activeTimer.requiredTime;
        }

        void UpdateCooldown()
        {
            slider.value = 1f - (_activeTimer.endTime - Time.time) /
                           _activeTimer.requiredTime;
        }

        void OffCooldown()
        {
            slider.value = 1f;
        }

        public void SetColor(Color color)
        {
            fill.color = color;
            fill.material = null;
        }

        public void SetRainbow()
        {
            fill.color = Color.white;
            fill.material = rainbowMat;
        }
        
    }
}
