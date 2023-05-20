using UnityEngine;

namespace Hullbreakers
{
    public abstract class EffectDeployer<T> : MonoBehaviour, IDamageFeedback, IColorable where T : EventFX<T>
    {
        bool _rainbow;
        Color _color;
        
        
        public void DeployFeedback(float resultDamage, Vector2 position, Vector2 direction)
        {
            T hitEffect = GenericPool<T>.instance.Get();
            
            hitEffect.SetColor(_rainbow ? ExtensionMethods.rainbow : _color);
            
            hitEffect.Spawn(position, direction.AngleFromDirection(), resultDamage);
        }

        public void SetColor(Color color)
        {
            _color = color;
            _rainbow = false;
        }

        public void SetRainbow()
        {
            _rainbow = true;
        }
    }
}
