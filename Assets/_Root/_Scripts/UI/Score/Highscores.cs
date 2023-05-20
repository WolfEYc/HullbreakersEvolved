using Doozy.Runtime.Signals;
using UnityEngine;

namespace Hullbreakers
{
    public class Highscores : Singleton<Highscores>
    {
        public RectTransform highScoreTextParent;
        public HighScoreText scoreTextPrefab;
        public int nScoresSaved = 3;
    
        int[] _scores;
        HighScoreText[] _scoreTextInstances;
        [SerializeField] SignalSender saveHighScoreSender;
        
        string GetInitials(int i)
        {
            return PlayerPrefs.GetString($"{DifficultyManager.instance.currentDifficultyLevel.ToString()}initials[{i}]", "AAA");
        }
    
        int GetScore(int i)
        {
            return PlayerPrefs.GetInt($"{DifficultyManager.instance.currentDifficultyLevel.ToString()}score[{i}]", 0);
        }

        void SetInitials(int i, string initials)
        {
            PlayerPrefs.SetString($"{DifficultyManager.instance.currentDifficultyLevel.ToString()}initials[{i}]", initials);
        }

        void SetScore(int i, int score)
        {
            PlayerPrefs.SetInt($"{DifficultyManager.instance.currentDifficultyLevel.ToString()}score[{i}]", score);
        }
    
        void Start()
        {
            InstantiateTextInstances();
            GameStateManager.instance.GameEnded += HandleGameEnd;
        }

        void InstantiateTextInstances()
        {
            _scores = new int[nScoresSaved];
            _scoreTextInstances = new HighScoreText[nScoresSaved];
            for(int i = 0; i < nScoresSaved; i++)
            {
                _scoreTextInstances[i] = Instantiate(scoreTextPrefab, highScoreTextParent);
            }
        }
        
        void LoadFromDisk()
        {
            for (int i = 0; i < nScoresSaved; i++)
            {
                _scores[i] = GetScore(i);
            
                _scoreTextInstances[i].scoreText.SetText(_scores[i].ToString());
                _scoreTextInstances[i].initialsText.SetText(GetInitials(i));
            }
        }

        bool CanSaveScore()
        {
            return ScoreManager.instance.score > _scores[nScoresSaved - 1];
        }
    
        public void SaveScore(string initials)
        {
            SaveScoreHelper(ScoreManager.instance.score, initials);
        }

        void SaveScoreHelper(int score, string initials ,int i = 0)
        {
            for (; i < nScoresSaved; i++)
            {
                if (score <= _scores[i]) continue;
            
                SetScore(i, score);
                SetInitials(i, initials);

                SaveScoreHelper(_scores[i], _scoreTextInstances[i].initialsText.text, i + 1);
                
                _scores[i] = score;
                _scoreTextInstances[i].scoreText.SetText(score.ToString());
                _scoreTextInstances[i].initialsText.SetText(initials);
                
                break;
            }
        }

        void HandleGameEnd()
        {
            LoadFromDisk();

            saveHighScoreSender.Payload.booleanValue = CanSaveScore();
            saveHighScoreSender.SendSignal();
        }
    }
}
