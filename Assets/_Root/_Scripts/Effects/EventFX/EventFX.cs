using UnityEngine;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    [RequireComponent(typeof(ColoredEffect))]
    public abstract class EventFX<T> : TimedPooledObject<T>, IColorable where T : EventFX<T>
    {
        static readonly ExposedProperty ScaleID = "Scale";

        [SerializeField] bool allowScaleModification;
        
        float scale
        {
            get => ColoredEffect.visualEffect.GetFloat(ScaleID);
            set => ColoredEffect.visualEffect.SetFloat(ScaleID, value);
        }

        protected ColoredEffect ColoredEffect;
        
        protected override void Awake()
        {
            base.Awake();
            ColoredEffect = GetComponent<ColoredEffect>();
        }

        public override void Spawn(Vector2 pos, float rot = 0f, float initialForce = 0f)
        {
            base.Spawn(pos, rot, initialForce);

            if (allowScaleModification)
            {
                scale = initialForce;
            }
            
            ColoredEffect.Play();
        }

        public void SetColor(Color color)
        {
            ColoredEffect.SetColor(color);
        }

        public void SetRainbow()
        {
            ColoredEffect.SetRainbow();   
        }
        public override float sizeRadius => scale;
    }
}
