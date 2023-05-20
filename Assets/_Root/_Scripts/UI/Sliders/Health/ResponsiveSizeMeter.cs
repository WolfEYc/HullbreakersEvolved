using Doozy.Runtime.Reactor;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(RectTransform), typeof(Progressor))]
    public class ResponsiveSizeMeter : MonoBehaviour
    {
        [SerializeField] Vector2 sizeRange;
        
        RectTransform _rectTransform;

        Vector2 _defaultSize;
        const float k_ReferenceValue = 100f;
        float _maxValue;
        Progressor _progressor;
        

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultSize = _rectTransform.sizeDelta;
            _progressor = GetComponent<Progressor>();
        }
        void UpdateSize()
        {
            _rectTransform.sizeDelta = new Vector2(Mathf.Clamp(_maxValue / k_ReferenceValue * _defaultSize.x,sizeRange.x,sizeRange.y), _defaultSize.y);
        }
        
        public void SetMaxValue(float maxValue)
        {
            _maxValue = maxValue;
            UpdateSize();
        }

        public void SetCurrentValue(float value)
        {
            _progressor.SetValueAt(value / _maxValue);
        }
    }
}
