using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(OnStayDamager))]
    public class VortexShot : Projectile<VortexShot>
    {
        public override int InflictDamage(IDamageable damageable, Vector2 position, Vector2 direction)
        {
            direction.Normalize();
            float resultDamage = damageable.Damage(Dmg, Vector2.zero);
            
            DamageFeedback(resultDamage, position, direction);

            if (WillIncDmgRecep)
            {
                damageable.IncreaseDamageReception(DmgRecepMultIncOnHit, DmgRecepBaseIncOnHit);
            }

            if (WillHeal)
            {
                FriendlyHealingSystem.DistributeHeals((int)(resultDamage * LifeSteal));
            }
            
            return (int)resultDamage;
        }
    }
}
