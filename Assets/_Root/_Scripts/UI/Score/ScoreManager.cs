using System;
using Doozy.Runtime.Signals;
using UnityEngine;

namespace Hullbreakers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int score { get; private set; }

        public event Action ScoreIncreased;
        [SerializeField] SignalSender scoreSender;

        void Start()
        {
            GameStateManager.instance.GameStarted += OnGameStarted;
        }

        void OnGameStarted()
        {
            score = 0;
        }

        public void IncreaseScore(int amt)
        {
            score += amt;
            ScoreIncreased?.Invoke();
            scoreSender.SendSignal();
        }
    }
}
