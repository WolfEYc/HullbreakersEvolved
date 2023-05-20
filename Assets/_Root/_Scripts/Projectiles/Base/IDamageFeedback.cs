using UnityEngine;

namespace Hullbreakers
{
    public interface IDamageFeedback
    {
        public void DeployFeedback(float resultDamage, Vector2 position, Vector2 direction);
    }
}
