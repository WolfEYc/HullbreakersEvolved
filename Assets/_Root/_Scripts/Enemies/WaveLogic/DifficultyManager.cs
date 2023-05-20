using System.Collections.Generic;
using Doozy.Runtime.UIManager;
using UnityEngine;

namespace Hullbreakers
{
    public class DifficultyManager : Singleton<DifficultyManager>
    {
        [SerializeField] Dictionary<UIButtonId.Difficulty, WaveList> _difficultySettings;

        public UIButtonId.Difficulty currentDifficultyLevel { get; private set; }
        public WaveList WaveList { get; private set; }
        
        public void SetDifficulty(UIButtonId.Difficulty difficulty)
        {
            currentDifficultyLevel = difficulty;
            WaveList = _difficultySettings[currentDifficultyLevel];
        }
        
        
    }
}
