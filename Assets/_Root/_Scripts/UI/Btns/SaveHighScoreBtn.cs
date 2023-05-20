using Doozy.Runtime.UIManager.Components;
using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(UIButton))]
    public class SaveHighScoreBtn : MonoBehaviour
    {
        [SerializeField] TMP_Text texttoSave;

        UIButton _button;

        void Awake()
        {
            _button = GetComponent<UIButton>();
            _button.onSelectedEvent.AddListener(SaveText);
        }
        
        void SaveText()
        {
            Highscores.instance.SaveScore(texttoSave.text);    
        }
    }
}
