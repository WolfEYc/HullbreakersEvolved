using UnityEngine;

namespace Hullbreakers
{
    public class PlacementRadius : MonoBehaviour
    {
        [field: SerializeField] public Multiplyable radius { get; private set; }
        Transform _transform;

        [SerializeField] SpriteRenderer[] indicators;
        [SerializeField] Transform ringTransform;

        void Awake()
        {
            _transform = transform;
            radius.OnValueChanged += RadiusOnValueChanged;
            radius.RefreshValue();
            RadiusOnValueChanged();
        }

        void RadiusOnValueChanged()
        {
            if(ringTransform == null) return;
            
            float diameter = radius.value * 2f;
            ringTransform.localScale = new Vector3(diameter, diameter, 1f);
        }

        public Vector2 position => _transform.position;

        public void SetColor(Color color)
        {
            foreach (SpriteRenderer indicator in indicators)
            {
                indicator.color = indicator.color.SetRGB(color);
            }
        }
        
        public void Toggle(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
