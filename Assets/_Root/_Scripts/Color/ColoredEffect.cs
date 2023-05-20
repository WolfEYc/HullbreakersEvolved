using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

namespace Hullbreakers
{
    [RequireComponent(typeof(VisualEffect))]
    public class ColoredEffect : MonoBehaviour, IColorable
    {
        public VisualEffect visualEffect { get; private set; }

        static readonly ExposedProperty ColorID = "Color";
        static readonly ExposedProperty RainbowMode = "RainbowMode";
        
        bool _rainbow;
        
        void Awake()
        {
            visualEffect = GetComponent<VisualEffect>();
        }
        
        public void SetColor(Color color)
        {
            visualEffect.SetVector4(ColorID, color);
            SetRainbow(false);
        }

        public void SetRainbow()
        {
            SetRainbow(true);
        }

        void SetRainbow(bool rainbow)
        {
            if(_rainbow == rainbow) return;
            _rainbow = rainbow;

            if (!enabled) return;
            
            Stop();
            Play();
        }

        public void Play()
        {
            if (_rainbow)
            {
                visualEffect.SendEvent(RainbowMode);
            }
            else
            {
                visualEffect.Play();
            }
        }

        public void Stop()
        {
            visualEffect.Stop();
        }
    }
}
