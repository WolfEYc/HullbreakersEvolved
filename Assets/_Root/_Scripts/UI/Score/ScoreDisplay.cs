using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreDisplay : MonoBehaviour
    {
        TMP_Text _text;

        void Awake()
        {
            _text = GetComponent<TMP_Text>();
            ScoreManager.instance.ScoreIncreased += OnScoreIncreased;
        }

        void OnScoreIncreased()
        {
            _text.SetText(ScoreManager.instance.score.ToString());
        }
    }
}
