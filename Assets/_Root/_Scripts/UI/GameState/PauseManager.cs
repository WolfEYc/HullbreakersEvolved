using Doozy.Runtime.Signals;
using UnityEngine;

namespace Hullbreakers
{
    public class PauseManager : MonoBehaviour
    {
        public bool paused { get; private set; }

        public void OnSignal(Signal signal)
        {
            if (!signal.TryGetValue(out bool value) || value == paused) return;
            
            paused = value;
            if (paused)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        void Pause()
        {
            
            Time.timeScale = 0f;
        }

        void Play()
        {
            Time.timeScale = 1f;
        }
        
        
    }
}
