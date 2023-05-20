using System;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(AbilityMods))]
    public class Ability : MonoBehaviour
    {
        public Timer durationTimer { get; private set; }
        public Timer countDownTimer { get; private set; }
        
        public event Action Used, Ended, OffCd;

        public AbilityMods mods { get; private set; }

        public Sprite image;

        public bool onCd { get; private set; }
        public bool active { get; private set; }

        protected virtual void Awake()
        {
            mods = GetComponent<AbilityMods>();
            durationTimer = gameObject.AddComponent<Timer>();
            countDownTimer = gameObject.AddComponent<Timer>();
            
            durationTimer.Expired += EndAbility;
            countDownTimer.Expired += RefreshCooldown;
            WaveManager.instance.WaveEnded += EndAbility;
            
            active = false;
            onCd = false;
        }
        
        public void Use()
        {
            if(onCd || active) return;
            ApplyAbility();
        }
        
        void ApplyAbility()
        {
            durationTimer.StartCountdown(mods.duration.value);
            active = true;
            Used?.Invoke();
        }

        public void EndAbility()
        {
            durationTimer.StopCountdown();
            countDownTimer.StartCountdown(mods.coolDownTime.value);
            active = false;
            onCd = true;
            Ended?.Invoke();
        }
        
        public void RefreshCooldown()
        {
            if(active) return;
            countDownTimer.StopCountdown();
            onCd = false;
            OffCd?.Invoke();
        }
    }
}
