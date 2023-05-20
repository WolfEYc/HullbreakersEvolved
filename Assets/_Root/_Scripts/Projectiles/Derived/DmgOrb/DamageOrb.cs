using System;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(ColoredEffect))]
    public class DamageOrb : PooledObject<DamageOrb>, IDamager, IColorable
    {
        [SerializeField] IDamageFeedback[] _damageFeedback;
        ColoredEffect _coloredEffect;

        protected override void Awake()
        {
            base.Awake();
            _coloredEffect = GetComponent<ColoredEffect>();
        }

        public override float sizeRadius => Transform.localScale.x;

        public Weapon weapon { get; set; }

        public void DamageHits()
        {
            Vector2 position = Transform.position;
            for (int i = 0; i < Explosion.Hits.Count; i++)
            {
                if(!Explosion.Hits[i].TryGetComponent(out IDamageable damageable)) return;

                
                Vector2 hitpos = Explosion.Hits[i].ClosestPoint(position);
                Vector2 dir = hitpos - position;

                InflictDamage(damageable, hitpos, dir);
            }
        }

        public int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir)
        {
            int resultDmg = (int)damageable.Damage(weapon.mods.damage.value, dir * weapon.mods.knockback.value);

            weapon.OnInflicDmg?.Invoke(damageable, resultDmg);
            
            for (int i = 0; i < _damageFeedback.Length; i++)
            {
                _damageFeedback[i].DeployFeedback(resultDmg, pos, dir);
            }

            return resultDmg;
        }

        void FixedUpdate()
        {
            Transform.RotateAround(
                weapon.Transform.position,
                Vector3.forward,
                weapon.mods.speed.value * Time.fixedDeltaTime);
        }

        public void SetColor(Color color)
        {
            _coloredEffect.SetColor(color);
        }

        public void SetRainbow()
        {
            _coloredEffect.SetRainbow();
        }
    }
}
