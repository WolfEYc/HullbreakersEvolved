using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(ExplosionDeployer))]
    public class Explosion : SerializedMonoBehaviour, ILaunchable, IActionOnDestroyed
    {
        ExplosionDeployer _explosionDeployer;
        ExplosionMods _explosionMods;
        WeaponMods _weaponMods;
        Transform _transform;

        ContactFilter2D _contactFilter;
        public static readonly List<Collider2D> Hits = new(69);

        [SerializeField] IDamageFeedback[] _onHitFeedbacks;

        void Awake()
        {
            _contactFilter.useLayerMask = true;
            _explosionDeployer = GetComponent<ExplosionDeployer>();
            _transform = transform;
        }

        public void OnDestroyed()
        {
            Vector2 pos = _transform.position;
            
            _explosionDeployer.DeployFeedback(
                _explosionMods.explosionRadius.value * 2f,
                pos, Vector2.zero);
            
            Physics2D.OverlapCircle(
                pos,
                _explosionMods.explosionRadius.value,
                _contactFilter,
                Hits);

            foreach (Collider2D enemy in Hits)
            {
                if (!enemy.TryGetComponent(out IDamageable splashDamageable)) continue;

                Vector2 hitPoint = enemy.ClosestPoint(pos);
                
                Vector2 splashDir = hitPoint - pos;
                splashDir.Normalize();
                
                float resultDmg = splashDamageable.Damage(
                    _explosionMods.explosionDamage.value,
                    splashDir * _weaponMods.knockback.value
                );

                foreach (IDamageFeedback damageFeedback in _onHitFeedbacks)
                {
                    damageFeedback.DeployFeedback(resultDmg, hitPoint, splashDir);
                }
            }
        }
        
        public void Launch(Weapon weapon, float angle)
        {
            _explosionMods = ((IBlastRadius)weapon).explosionMods;
            _weaponMods = weapon.mods;
            _contactFilter.layerMask = _weaponMods.layerMask;
        }
    }
}
