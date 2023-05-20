using UnityEngine;

namespace Hullbreakers
{
    public class ShipDeathEffect : MonoBehaviour, IActionOnDestroyed
    {
        [SerializeField] SpriteRenderer shipSpriteRenderer;
        [SerializeField] float scale = 1;

        
        public void OnDestroyed()
        {
            DeathFX deathFX = DeathFxPool.instance.Get();
            
            deathFX.SetColor(shipSpriteRenderer.color);
            
            deathFX.Spawn(
                transform.position,
                0f,
                scale);
        }
    }
}
