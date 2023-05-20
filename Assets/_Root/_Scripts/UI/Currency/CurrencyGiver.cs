using UnityEngine;

namespace Hullbreakers
{
    public class CurrencyGiver : MonoBehaviour, IActionOnDestroyed
    {
        [SerializeField] int generosity;

        const float k_OrbTier = 0.8f, k_RerollTier = 0.9f;
        
        public void OnDestroyed()
        {
            if(GameStateManager.instance.state != GameStateManager.GameState.InGame) return;
            
            for (int i = 0; i < generosity; i++)
            {
                Donate();
            }
        }

        void Donate()
        {
            switch (Random.value)
            {
                case < k_OrbTier:
                    OrbPool.instance.Get().Spawn(transform.position);
                    break;
                case < k_RerollTier:
                    RerollPool.instance.Get().Spawn(transform.position);
                    break;
                default:
                    CashPool.instance.Get().Spawn(transform.position);
                    break;
            }
        }
    }
}
