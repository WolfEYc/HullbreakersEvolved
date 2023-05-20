using UnityEngine;

namespace Hullbreakers
{
    public class Playlist : MonoBehaviour
    {
        [SerializeField] AudioClip[] songs;
        [SerializeField] bool playOnEnable;
        int _idx;

        void Awake()
        {
            songs.Shuffle();
        }
        void OnEnable()
        {
            if(!playOnEnable) return;
            Play();
        }

        public void Stop()
        {
            AudioManager.instance.MusicFinished -= PlayNext;
        }

        public void Play()
        {
            AudioManager.instance.MusicFinished += PlayNext;
            PlayNext();
        }

        void PlayNext()
        {
            AudioManager.instance.PlayMusic(songs[_idx++ % songs.Length]);  
        }
    }
}
