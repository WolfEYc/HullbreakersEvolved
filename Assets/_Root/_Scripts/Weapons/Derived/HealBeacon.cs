using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(HealArcDeployer))]
    public class HealBeacon : Weapon, IColorable
    {
        HealArcDeployer _fxdeployer;

        protected override void Awake()
        {
            base.Awake();
            _fxdeployer = gameObject.GetComponent<HealArcDeployer>();
            OnStoppedShooting += _fxdeployer.DeactivateArcs;
        }
        
        public void SetColor(Color color)
        {
            _fxdeployer.SetColor(color);
        }
        public void SetRainbow()
        {
            _fxdeployer.SetRainbow();
        }
        
        public override void Shoot()
        {
            int healedTargets = friendlyHealingSystem.DistributeHeals((int)mods.lifeSteal.value);
            
            for(int i = 0; i < healedTargets; i++)
            {
                _fxdeployer.StampOrAdd(friendlyHealingSystem.cpy[i].simpleDamageRefs.hitBox, friendlyHealingSystem.cpy[i]);
            }
            
            _fxdeployer.RemoveNonStamped();
            muzzleFlash.Play();
        }
    }
}
