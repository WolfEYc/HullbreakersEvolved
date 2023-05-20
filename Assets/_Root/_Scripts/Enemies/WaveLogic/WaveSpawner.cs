using System.Collections;
using UnityEngine;


namespace Hullbreakers
{
    public class WaveSpawner : Singleton<WaveSpawner>
    {
        int _currentWave;
        int _currentEncounter;

        bool _inWave;

        Timer _timer;

        protected override void Awake()
        {
            base.Awake();
            _timer = gameObject.AddComponent<Timer>();
            _timer.Expired += EndWave;
        }
        
        void Start()
        {
            GameStateManager.instance.GameEnded += OnGameEnded;
            GameStateManager.instance.OnClear += Clear;
            WaveManager.instance.WaveStarted += StartWave;
            EnemiesManager.instance.EnemiesReachedZero += EndWave;
        }

        void OnGameEnded()
        {
            _timer.StopCountdown();
            _inWave = false;
            StopAllCoroutines();
        }

        void Clear()
        {
            _currentWave = 0;
            _currentEncounter = 0;
        }

        void StartWave()
        {
            StartCoroutine(SpawnWave(DifficultyManager.instance.WaveList.waves[_currentWave++]));
        }

        void EndWave()
        {
            if(_inWave) return;
            _timer.StopCountdown();
            BlackHole.instance.Summon();
        }

        IEnumerator SpawnWave(Wave wave)
        {
            _inWave = true;
            for (_currentEncounter = 0; _currentEncounter < wave.encounters.Count; _currentEncounter++)
            {
                wave.encounters[_currentEncounter].Spawn();
                
                yield return new WaitForSeconds(wave.encounters[_currentEncounter].delayForNextEncounter);
                if (WaveManager.instance.currentWaveState == WaveManager.WaveState.InWave) continue;
                _inWave = false;
                yield break;
            }
            
            _inWave = false;
            if (EnemiesManager.instance.totalEnemies == 0)
            {
                EndWave();
            }
            else
            {
                _timer.StartCountdown(wave.ttkwave);   
            }
        }
    }
}
