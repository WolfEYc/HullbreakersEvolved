using UnityEngine;

namespace Hullbreakers
{
    public abstract class ArcWeapon<T> : ZoneWeapon, IColorable where T : ArcFX<T>
    {
        [SerializeField] ArcDeployer<T> fxdeployer;

        protected override void Awake()
        {
            base.Awake();
            OnStoppedShooting += fxdeployer.DeactivateArcs;
        }
        
        public override void Shoot()
        {
            base.Shoot();
            
            for (int i = 0; i < hits; i++)
            {
                Health health = results[i].GetComponent<SimpleDamageRefs>().hp;
                if(health.dead) return;
                fxdeployer.StampOrAdd(results[i], health);
            }
            
            fxdeployer.RemoveNonStamped();
        }
        
        public void SetColor(Color color)
        {
            fxdeployer.SetColor(color);
        }

        public void SetRainbow()
        {
            fxdeployer.SetRainbow();
        }
    }
}
