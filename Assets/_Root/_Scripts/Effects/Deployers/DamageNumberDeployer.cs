using UnityEngine;

namespace Hullbreakers
{
    public class DamageNumberDeployer : MonoBehaviour, IDamageFeedback
    {
        public void DeployFeedback(float resultDamage, Vector2 position, Vector2 direction)
        {
            if(!this.AmPlayerAttack()) return;
            DamageNumberPool.instance.Get().Deploy((int)resultDamage, position, direction);
        }
    }
}
