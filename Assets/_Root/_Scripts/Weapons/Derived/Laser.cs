using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : RaycastWeapon
    {
        LineRenderer _lr;
        float _defaultLen;

        protected override void Awake()
        {
            base.Awake();
            _lr = GetComponent<LineRenderer>();
            
            OnStartedShooting += HandleStartedShooting;
            OnStoppedShooting += HandleStoppedShooting;
            
            mods.spread.OnValueChanged += ResizeLineRendererWidth;
            ResizeLineRendererWidth();
        }

        void ResizeLineRendererWidth()
        {
            _lr.widthMultiplier = mods.spread.value;
        }

        void HandleStartedShooting()
        {
            muzzleFlash.Play();
            _lr.enabled = true;
        }
        
        void HandleStoppedShooting()
        {
            muzzleFlash.Stop();
            _lr.enabled = false;
        }
        
        public override void Shoot()
        {
            base.Shoot();
            _lr.SetPosition(1, Endpos);
        }
        
    }
}
