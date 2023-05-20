using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(LineRenderer))]
    public class ColoredLineRenderer : MonoBehaviour, IColorable
    {
        LineRenderer _lr;
        [SerializeField] Material rainbowMat;

        Material _defaultMat;
        
        void Awake()
        {
            _lr = GetComponent<LineRenderer>();
            _defaultMat = _lr.material;
        }


        public void SetColor(Color color)
        {
            _lr.material = _defaultMat;
            _lr.startColor = color;
            _lr.endColor = color;
        }

        public void SetRainbow()
        {
            _lr.material = rainbowMat;
        }
    }
}
