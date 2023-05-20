using System;
using UnityEngine;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    public abstract class ProjectileWeapon<T> : Weapon, IColorable where T : Projectile<T>
    {
        public Color color { get; private set; }
        public bool rainbow { get; private set; }

        static readonly ExposedProperty VelocityID = "Velocity";

        event Action OnShoot;

        protected override void Awake()
        {
            base.Awake();
            if (muzzleFlash != null)
            {
                OnShoot += DeployMuzzleFlash;
            }
        }

        public override void Shoot()
        {
            float half = (mods.count.value - 1) / 2f;
            float z = Transform.eulerAngles.z;
            
            for (int i = 0; i < mods.count.value; i++)
            {
                float diff = i - half;
                GenericPool<T>.instance.Get().
                    Launch(this, z + diff * mods.spread.value);
            }
            
            OnShoot?.Invoke();
        }

        void DeployMuzzleFlash()
        {
            muzzleFlash.visualEffect.SetVector2(VelocityID, centerOfMass.velocity);
            muzzleFlash.Play();
        }
        
        
        public void SetColor(Color newColor)
        {
            rainbow = false;
            color = newColor;
        }

        public void SetRainbow()
        {
            rainbow = true;
        }
    }
}
