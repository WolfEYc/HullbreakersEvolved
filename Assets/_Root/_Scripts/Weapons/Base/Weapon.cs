using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public abstract class Weapon : SerializedMonoBehaviour, IPlayerControllable, IActionOnDestroyed
    {
        [SerializeField] protected ColoredEffect muzzleFlash;
        public Rigidbody2D centerOfMass { get; private set; }
        
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public WeaponMods mods { get; private set; }

        bool _playerControlled;
        bool _shooting;
        
        public bool willStun { get; private set; }
        public bool willHeal { get; private set; }
        public bool willIncDmgRecep { get; private set; }
        

        WaitForSeconds _waitForShot;
        WaitUntil _isShooting;
        WaitUntil _waitUntilshooting;
        LocalTimeScale _localTimeScale;

        IEnumerator _shootRoutine;
        
        public event Action OnStartedShooting, OnStoppedShooting;
        [HideInInspector] public Action<IDamageable, int> OnInflicDmg;
        public HealingSystem friendlyHealingSystem { get; private set; }

        [SerializeField] bool autoFire;
        
        protected virtual void Awake()
        {
            _localTimeScale = GetComponentInParent<LocalTimeScale>();

            _localTimeScale.timeScale.OnValueChanged += UpdateWaitForShot;
            mods = GetComponentInParent<WeaponMods>();
            centerOfMass = GetComponentInParent<Rigidbody2D>();

            Transform = transform;
            GameObject = gameObject;
            mods.shotsPerMinute.OnValueChanged += UpdateWaitForShot;
            _waitUntilshooting = new WaitUntil(() => _shooting);
            _waitForShot = new WaitForSeconds(0f);
            
            mods.stunDuration.OnValueChanged += AddStunEffect;
            mods.dmgRecepIncOnTarget.OnValueChanged += AddDmgIncEffect;
            mods.lifeSteal.OnValueChanged += AddLifeStealEffect;
            
            friendlyHealingSystem = this.AmPlayerAttack()
                ? HealingSystems.instance.playerHeals
                : HealingSystems.instance.enemyHeals;
            
        }

        protected virtual void Start()
        {
            UpdateWaitForShot();
            AddStunEffect();
            AddDmgIncEffect();
            AddLifeStealEffect();
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(ShootRoutine());
            if (!autoFire) return;
            
            WaveManager.instance.WaveStarted += StartShooting;
            WaveManager.instance.WaveEnded += StopShooting;
        }

        void OnDisable()
        {
            StopCoroutine(ShootRoutine());
        }

        void UpdateWaitForShot()
        {
            _waitForShot = new WaitForSeconds(60f / (mods.shotsPerMinute.value * _localTimeScale.timeScale.value));
        }
        
        IEnumerator ShootRoutine()
        {
            while (true)
            {
                yield return _waitForShot;
                yield return _waitUntilshooting;
                if(!_shooting) continue;
                Shoot();
            }
        }

        public abstract void Shoot();

        public void StartShooting()
        {
            if(_shooting) return;
            _shooting = true;
            OnStartedShooting?.Invoke();
        }

        public void DealStun(IDamageable damageable)
        {
            damageable.Stun(mods.stunDuration.value);
        }

        public void HealFriendlies(int heals)
        {
            friendlyHealingSystem.DistributeHeals(heals);
        }

        public void IncDmgReception(IDamageable damageable)
        {
            damageable.IncreaseDamageReception(mods.dmgRecepIncOnTarget.Multiplier, mods.dmgRecepIncOnTarget.BaseValue);
        }
        
        public void StopShooting()
        {
            if(!_shooting) return;
            _shooting = false;
            OnStoppedShooting?.Invoke();
        }
        public void SetPlayerControl()
        {
            if(autoFire) return;
            PlayerManager.instance.playerInput.shooting.TurnedOn += StartShooting;
            PlayerManager.instance.playerInput.shooting.TurnedOff += StopShooting;
            _playerControlled = true;
        }
        
        public void RemovePlayerControl()
        {
            if(autoFire) return;
            PlayerManager.instance.playerInput.shooting.TurnedOn -= StartShooting;
            PlayerManager.instance.playerInput.shooting.TurnedOff -= StopShooting;
            _playerControlled = false;
            StopShooting();
        }
        
        public virtual void OnDestroyed()
        {
            if (_playerControlled)
            {
                RemovePlayerControl();
            }
            else
            {
                StopShooting();
            }
            
            if (!autoFire) return;
            
            WaveManager.instance.WaveStarted -= StartShooting;
            WaveManager.instance.WaveEnded -= StopShooting;
        }

        void AddStunEffect()
        {
            if (!(mods.stunDuration.value > 0f)) return;
            
            OnInflicDmg += (damageable, _) => DealStun(damageable);
            willStun = true;
        }

        void AddDmgIncEffect()
        {
            if (Mathf.Approximately(mods.dmgRecepIncOnTarget.value, 0f)) return;
            
            OnInflicDmg += (damageable, _) => IncDmgReception(damageable);
            willIncDmgRecep = true;
        }

        void AddLifeStealEffect()
        {
            if (Mathf.Approximately(mods.lifeSteal.value, 0f)) return;

            willHeal = true;
            
            OnInflicDmg += (_, dmg) => HealFriendlies(dmg);
        }
    }
}
