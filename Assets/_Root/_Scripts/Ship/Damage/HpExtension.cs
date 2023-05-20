using UnityEngine;

namespace Hullbreakers
{
    public class HpExtension : MonoBehaviour, IDamageable
    {
        [SerializeField] Health mainBody;
        [SerializeField] Knockback knockbackable;
        
        public float damageScalar = 1f;
        

        public float Damage(float damage, Vector2 knockbackForce)
        {
            if (knockbackable != null)
            {
                knockbackable.ApplyKnockback(knockbackForce);
                knockbackForce = Vector2.zero;
            }
            
            return mainBody.Damage(damage * damageScalar, knockbackForce);
        }

        public float CrashDamage(float crashDmg)
        {
            return mainBody.CrashDamage(crashDmg);
        }

        public float Heal(float health)
        {
            return mainBody.Heal(health);
        }

        public float Stun(float time)
        {
            return mainBody.Stun(time);
        }

        public void IncreaseDamageReception(float multiplierInc, float baseValueInc)
        {
            mainBody.IncreaseDamageReception(multiplierInc, baseValueInc);
        }
    }
}
