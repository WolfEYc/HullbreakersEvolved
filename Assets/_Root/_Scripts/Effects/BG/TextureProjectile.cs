using UnityEngine;

namespace Hullbreakers
{
    public class TextureProjectile : Projectile<TextureProjectile>
    {
        [SerializeField] SpriteRenderer projectileVisuals;
        
        public override void Launch(ProjectileWeapon<TextureProjectile> launcher, float angle)
        {
            base.Launch(launcher, angle);
            projectileVisuals.sprite = ((TextureMinigun)launcher).textures.RandomElement();
        }
    }
}
