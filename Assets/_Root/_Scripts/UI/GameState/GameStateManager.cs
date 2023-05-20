using System;
using Doozy.Runtime.Signals;
using UnityEngine;

namespace Hullbreakers
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public enum GameState
        {
            Standby,
            InGame
        }

        [SerializeField] SignalSender startSignal, endSignal;
        
        public event Action GameStarted;
        public event Action GameEnded;
        public event Action OnClear;

        public GameState state { get; private set; } = GameState.Standby;
        
        
        public void EndGame()
        {
            if(state == GameState.Standby) return;
            state = GameState.Standby;
            endSignal.SendSignal();
            GameEnded?.Invoke();
        }
        
        public void StartGame()
        {
            if(state == GameState.InGame) return;
            state = GameState.InGame;
            startSignal.SendSignal();
            GameStarted?.Invoke();
        }

        public void Clear()
        {
            OnClear?.Invoke();
        }
    }
}
