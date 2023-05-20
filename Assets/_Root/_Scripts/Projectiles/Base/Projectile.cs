using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public abstract class Projectile<T> : TimedPooledObject<T>, IDamager where T : Projectile<T>
    {
        Rigidbody2D rb { get; set; }

        CircleCollider2D _collider;
        
        ProjectileWeapon<T> owner { get; set; }
        
        IColorable[] _colorables;
        ILaunchable[] _launchables;
        
        [SerializeField] IDamageFeedback[] _damageFeedbacks;

        int _pierce;
        protected float Dmg;
        float _kbForce;
        float _stun;
        protected float LifeSteal;
        protected float DmgRecepBaseIncOnHit;
        protected float DmgRecepMultIncOnHit;
        protected bool WillStun, WillIncDmgRecep, WillHeal;
        protected HealingSystem FriendlyHealingSystem;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CircleCollider2D>();
            
            _colorables = GetComponentsInChildren<IColorable>();
            _launchables = GetComponentsInChildren<ILaunchable>();
        }

        public virtual void Launch(ProjectileWeapon<T> launcher, float angle)
        {
            owner = launcher;
            CpyOwnerValues();
            
            Spawn(owner.Transform.position, angle, owner.mods.speed.value);
            
            Timer.StartCountdown(owner.mods.ttl.value);
            
            if (owner.rainbow)
            {
                SetRainbow();
            }
            else
            {
                SetColor(owner.color);
            }

            foreach (ILaunchable launchable in _launchables)
            {
                launchable.Launch(launcher, angle);
            }
        }

        void CpyOwnerValues()
        {
            GameObject.SetLayerRecursively(owner.GameObject.layer);
            
            Transform.localScale = Vector3.one * owner.mods.size.value;
            rb.velocity += owner.centerOfMass.velocity;
            
            _pierce = owner.mods.pierce.value;
            Dmg = owner.mods.damage.value;
            _kbForce = owner.mods.knockback.value;
            _stun = owner.mods.stunDuration.value;
            DmgRecepBaseIncOnHit = owner.mods.dmgRecepIncOnTarget.BaseValue;
            DmgRecepMultIncOnHit = owner.mods.dmgRecepIncOnTarget.Multiplier;
            LifeSteal = owner.mods.lifeSteal.value;
            
            WillStun = owner.willStun;
            WillIncDmgRecep = owner.willIncDmgRecep;
            WillHeal = owner.willHeal;
            FriendlyHealingSystem = owner.friendlyHealingSystem;
        }
        
        public virtual int InflictDamage(IDamageable damageable, Vector2 position, Vector2 direction)
        {
            direction.Normalize();
            float resultDamage = damageable.Damage(Dmg, direction * _kbForce);
            
            DamageFeedback(resultDamage, position, direction);
            
            if (WillStun)
            {
                damageable.Stun(owner.mods.stunDuration.value);
            }

            if (WillIncDmgRecep)
            {
                damageable.IncreaseDamageReception(DmgRecepMultIncOnHit, DmgRecepBaseIncOnHit);
            }

            if (WillHeal)
            {
                FriendlyHealingSystem.DistributeHeals((int)(resultDamage * LifeSteal));
            }
            
            _pierce--;

            if (_pierce >= 0) return (int)resultDamage;
            
            Release();

            return (int)resultDamage;
        }
        
        protected void DamageFeedback(float resultDamage, Vector2 position, Vector2 direction)
        {
            foreach (IDamageFeedback damageFeedback in _damageFeedbacks)
            {
                damageFeedback.DeployFeedback(resultDamage, position, direction);
            }
        }
        
        public void SetColor(Color color)
        {
            foreach (IColorable colorable in _colorables)
            {
                colorable.SetColor(color);
            }
        }

        public void SetRainbow()
        {
            foreach (IColorable colorable in _colorables)
            {
                colorable.SetRainbow();
            }
        }

        public override void Spawn(Vector2 pos, float rot = 0f, float initialForce = 0f)
        {
            base.Spawn(pos, rot, initialForce);
            rb.velocity = rot.DirectionFromAngleDegrees() * initialForce;
        }

        public override float sizeRadius => _collider.radius;
    }
}
