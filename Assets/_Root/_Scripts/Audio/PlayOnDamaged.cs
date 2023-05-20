using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Health), typeof(AudioSource))]
    public class PlayOnDamaged : MonoBehaviour
    {
        Health _health;
        AudioSource _audioSource;

        void Awake()
        {
            _health = GetComponent<Health>();
            _audioSource = GetComponent<AudioSource>();
            
            _health.OnDamaged += _ =>
            {
                if(_audioSource.isPlaying) return;
                _audioSource.Play();
            };
        }
    }
}
