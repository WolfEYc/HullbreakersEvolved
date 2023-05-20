using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public struct VolumeSetter
    {
        [SerializeField] string mixerVol;

        const float k_MinVol = -80f, k_MaxVol = 20f;

        float NormalizeVolume(float denormalizedVolume)
        {
            return denormalizedVolume.Remap(k_MinVol, k_MaxVol, 0f, 1f);
        }

        float DenormalizeVolume(float normalizedVolume)
        {
            return normalizedVolume.Remap(0f, 1f, k_MinVol, k_MaxVol);
        }
        
        public void SetVolume(float volume)
        {
            AudioManager.instance.Mixer.SetFloat(mixerVol, DenormalizeVolume(volume));
        }

        public float GetVolume()
        {
            AudioManager.instance.Mixer.GetFloat(mixerVol, out float vol);
            return NormalizeVolume(vol);
        }
        

        public VolumeSetter(string mixerVol)
        {
            this.mixerVol = mixerVol;
        }
    }
}
