using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BlackHole : Singleton<BlackHole>
    {
        GameObject _gameObject;
        Transform _transform;
        Animator _animator;
        static readonly int SummonID = Animator.StringToHash("summon");
        
        
        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
            _gameObject = gameObject;
            _animator = GetComponent<Animator>();
            _gameObject.SetActive(false);
            GameStateManager.instance.GameEnded += Clear;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            PlayerManager.instance.playerShipControl.RemovePlayerControl();
            DroneManager.instance.RemovePlayerControl();

            var playerRb = PlayerManager.instance.playerRb;
            
            playerRb.velocity = Vector2.zero;
            playerRb.position = transform.position;
        }

        void Clear()
        {
            _transform.localScale = Vector3.zero;
            _gameObject.SetActive(false);
        }

        public void EndWave()
        {
            _gameObject.SetActive(false);
            WaveManager.instance.EndWave();
        }

        public void Summon()
        {
            if(WaveManager.instance.currentWaveState == WaveManager.WaveState.Standby) return;
            _gameObject.SetActive(true);
            _animator.SetTrigger(SummonID);
        }
    }
}
