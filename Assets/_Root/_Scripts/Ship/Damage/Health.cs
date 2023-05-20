using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hullbreakers
{
    [RequireComponent(typeof(HealthMods))]
    public class Health : SerializedMonoBehaviour, IDamageable, IActionOnDestroyed
    {
        Knockback _knockback;

        public event Action OnKilled;
        public event Action<Health> Destroyed;
        public event Action<float> OnHealed;
        public event Action<float> OnDamaged;
        public event Action<float> OnStunned;
        public event Action onBecameInvincible;
        public event Action onLostInvincible;
        
        public float hp { get; private set; }

        public bool dead { get; private set; }

        [SerializeField] bool _invincible;

        [SerializeField] Health passthrough;

        public UnityEvent onDestroyedSerialized;
        bool _passthrough;

        HealingSystem _healingSystem;
        public SimpleDamageRefs simpleDamageRefs { get; private set; }
        
        public bool invincible
        {
            get => _invincible;
            set
            {
                if(_invincible == value) return;
                _invincible = value;
                if (_invincible)
                {
                    onBecameInvincible?.Invoke();
                }
                else
                {
                    onLostInvincible?.Invoke();
                }
            }
        }

        public HealthMods healthMods { get; private set; }
        
        void Awake()
        {
            healthMods = GetComponent<HealthMods>();
            _passthrough = passthrough != null;
            _healingSystem = this.AmPlayer() ? 
                HealingSystems.instance.playerHeals : 
                HealingSystems.instance.enemyHeals;
            simpleDamageRefs = GetComponentInParent<SimpleDamageRefs>();
        }

        void Start()
        {
            hp = healthMods.maxHp.value;
            OnHealed?.Invoke(hp);
        }

        void OnEnable()
        {
            Resurrect();
            _healingSystem.toHeal.Add(this);
        }

        public float Damage(float damage, Vector2 knockbackForce)
        {
            if (_invincible) return 0f;

            if (dead)
            {
                return DamagePassthrough(damage, knockbackForce);
            }
            
            if (_knockback != null)
            {
                _knockback.ApplyKnockback(knockbackForce);
            }

            float resultDmg = (damage + healthMods.damageReceptionScalar.BaseValue) * healthMods.damageReceptionScalar.Multiplier;

            hp -= resultDmg;
            
            OnDamaged?.Invoke(resultDmg);

            if (hp <= 0f)
            {
                Kill();
            }

            return resultDmg;
        }

        public float CrashDamage(float crashDmg)
        {
            return Damage(healthMods.crashDmgReceptionScalar.Multiplier * crashDmg +
                          healthMods.crashDmgReceptionScalar.BaseValue, Vector2.zero);
        }

        float DamagePassthrough(float damage, Vector2 knockbackForce)
        {
            return _passthrough ? passthrough.Damage(damage, knockbackForce) : 0f;
        }
        
        public void Kill()
        {
            if(dead) return;
            dead = true;
            hp = 0f;
            
            OnKilled?.Invoke();
            onDestroyedSerialized.Invoke();
        }
        
        public float Heal(float healing)
        {
            dead = false;

            float resultHeal = (healing + healthMods.healingReceptionScalar.value) * healthMods.healingReceptionScalar.Multiplier;

            float prevHp = hp;
            hp = Mathf.Min(hp + resultHeal, healthMods.maxHp.value);

            float diff = hp - prevHp;
            
            OnHealed?.Invoke(diff);

            return diff;
        }

        public void SetHp(float newHp)
        {
            hp = Mathf.Min(newHp, healthMods.maxHp.value);
            
            OnHealed?.Invoke(hp);
        }

        public float Stun(float time)
        {
            if (_invincible) return 0f;
            float resultStun = healthMods.stunReceptionScalar.Multiplier * time + healthMods.stunReceptionScalar.value;
            
            OnStunned?.Invoke(resultStun);
            
            return resultStun;
        }

        public void IncreaseDamageReception(float multiplierInc, float baseValueInc)
        {
            healthMods.damageReceptionScalar.Multiplier += multiplierInc;
            healthMods.damageReceptionScalar.BaseValue += baseValueInc;
        }

        public void Resurrect()
        {
            healthMods.damageReceptionScalar.Reset(0f);
            hp = healthMods.maxHp.value;
            dead = false;
            invincible = _invincible;
            OnHealed?.Invoke(hp);
        }

        public void OnDestroyed()
        {
            dead = true;
            _healingSystem.toHeal.Remove(this);
            Destroyed?.Invoke(this);
        }
    }
}
