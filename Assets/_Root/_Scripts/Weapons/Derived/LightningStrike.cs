using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    public class LightningStrike : RaycastWeapon
    {
        static readonly ExposedProperty LengthID = "Length";
        
        public override void Shoot()
        {
            base.Shoot();
            
            muzzleFlash.visualEffect.SetFloat(LengthID, Endpos.x);
            muzzleFlash.Play();
        }
    }
}
