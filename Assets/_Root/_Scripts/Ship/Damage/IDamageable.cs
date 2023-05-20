using UnityEngine;

namespace Hullbreakers
{
    public interface IDamageable
    {
        public float Damage(float damage, Vector2 knockbackForce);
        public float CrashDamage(float crashDmg);
        public float Heal(float health);
        public float Stun(float time);
        public void IncreaseDamageReception(float multiplierInc, float baseValueInc);
    }
}
