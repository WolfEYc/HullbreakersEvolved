using System;
using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public class WaveManager : Singleton<WaveManager>
    {
        public event Action WaveEnded;
        public event Action WaveStarted;

        [SerializeField] SignalSender endWaveSender;

        public enum WaveState
        {
            InWave,
            Standby
        }
        
        
        void Start()
        {
            GameStateManager.instance.GameEnded += OnGameEnded;
            GameStateManager.instance.GameStarted += StartWave;
        }

        void OnGameEnded()
        {
            currentWaveState = WaveState.Standby;
        }

        public WaveState currentWaveState { get; private set; } = WaveState.Standby;
        
        [Button]
        public void EndWave()
        {
            if(currentWaveState == WaveState.Standby) return;
            currentWaveState = WaveState.Standby;

            WaveEnded?.Invoke();
            endWaveSender.SendSignal();
        }

        public void StartWave()
        {
            if(currentWaveState == WaveState.InWave) return;
            currentWaveState = WaveState.InWave;
            WaveStarted?.Invoke();
        }
    }
}
