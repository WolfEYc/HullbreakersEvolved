using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(UnitReference))]
    public class ColorController : MonoBehaviour
    {
        UnitSo _unitReference;
        IColorable[] _colorables;

        void Awake()
        {
            _unitReference = GetComponent<UnitReference>().selfUnit;
        }

        public void RefreshColorables()
        {
            _colorables = GetComponentsInChildren<IColorable>();
            SetColor(_unitReference.color);
        }

        void Start()
        {
            RefreshColorables();
        }

        public void SetColor(Color color)
        {
            foreach (IColorable colorable in _colorables)
            {
                colorable.SetColor(color);
            }
        }

        public void SetRainbow()
        {
            foreach (IColorable colorable in _colorables)
            {
                colorable.SetRainbow();
            }
        }
    }
}
