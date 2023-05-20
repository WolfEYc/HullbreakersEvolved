using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColorableSprite : MonoBehaviour, IColorable
    {
        SpriteRenderer _spriteRenderer;
        [SerializeField] Material rainbowMat;
        Material _default;
        
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _default = _spriteRenderer.material;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.material = _default;
            _spriteRenderer.color = _spriteRenderer.color.SetRGB(color);
        }

        public void SetRainbow()
        {
            _spriteRenderer.color = Color.white;
            _spriteRenderer.material = rainbowMat;
        }
    }
}
