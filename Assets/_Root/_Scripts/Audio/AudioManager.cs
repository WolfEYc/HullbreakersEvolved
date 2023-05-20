using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Hullbreakers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] AudioMixer mixer;
        public AudioMixer Mixer => mixer;

        [SerializeField] AudioSource music, sounds;

        Transform _soundsTransform;
        Vector3 _defaultSoundpos;

        WaitUntil _waitUntilMusicDone;

        public event Action MusicFinished;
        IEnumerator _pingRoutine;
        
        protected override void Awake()
        {
            base.Awake();
            _soundsTransform = sounds.transform;
            _defaultSoundpos = _soundsTransform.position;
            _waitUntilMusicDone = new WaitUntil(() => !music.isPlaying);
        }

        public void PlayMusic(AudioClip clip, float vol = 0.5f, bool loop = false)
        {
            music.volume = vol;
            music.clip = clip;
            music.loop = loop;
            music.Play();
            if(_pingRoutine != null) return;
            StartCoroutine(_pingRoutine = PingWhenFinished());
        }

        public void StopMusic()
        {
            if (_pingRoutine != null)
            {
                StopCoroutine(_pingRoutine);
                _pingRoutine = null;
            }
            
            music.Stop();
            MusicFinished?.Invoke();
        }

        public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1f)
        {
            _soundsTransform.position = pos;
            PlayOneShot(clip, vol);
        }

        public void PlaySound(AudioClip clip, float vol = 1f)
        {
            _soundsTransform.position = _defaultSoundpos;
            PlayOneShot(clip, vol);
        }
    
        void PlayOneShot(AudioClip clip, float vol)
        {
            sounds.PlayOneShot(clip, vol);
        }

        IEnumerator PingWhenFinished()
        {
            yield return _waitUntilMusicDone;
            _pingRoutine = null;
            MusicFinished?.Invoke();
        }
    }
}
