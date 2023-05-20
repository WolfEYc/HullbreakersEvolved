using UnityEngine;

namespace Hullbreakers
{
    public interface IDamager
    {
        public int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir);
    }
}
