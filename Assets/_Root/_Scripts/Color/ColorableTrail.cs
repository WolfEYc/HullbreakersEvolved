using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TrailRenderer))]
    public class ColorableTrail : MonoBehaviour, IColorable, IActionOnDestroyed
    {
        TrailRenderer _trailRenderer;
        [SerializeField] Material rainbowMat;

        Material _defaultMat;
        
        void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _defaultMat = _trailRenderer.material;
        }

        public void SetColor(Color color)
        {
            _trailRenderer.material = _defaultMat;
            _trailRenderer.startColor = _trailRenderer.startColor.SetRGB(color);
            _trailRenderer.endColor = _trailRenderer.endColor.SetRGB(color);
        }

        public void SetRainbow()
        {
            _trailRenderer.material = rainbowMat;
        }

        public void OnDestroyed()
        {
            _trailRenderer.Clear();
        }
    }
}
