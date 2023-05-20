using UnityEngine;

namespace Hullbreakers
{
    public class Peashot : Projectile<Peashot>
    {
        [SerializeField] SpriteRenderer projectileVisuals;
        
        public override void Launch(ProjectileWeapon<Peashot> launcher, float angle)
        {
            base.Launch(launcher, angle);
            projectileVisuals.sprite = ((Peashooter)launcher).projectileSprite;
        }
    }
}
