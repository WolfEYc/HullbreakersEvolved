using System;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(UIButton))]
    public class DifficultyButton : MonoBehaviour
    {
        UIButton _button;
        
        void Awake()
        {
            _button = GetComponent<UIButton>();
        }

        void Start()
        {
            _button.onSelectedEvent.AddListener(SetDifficulty);
            _button.onSelectedEvent.AddListener(GameStateManager.instance.StartGame);
        }

        void SetDifficulty()
        {
            DifficultyManager.instance.SetDifficulty((UIButtonId.Difficulty)Enum.Parse(typeof(UIButtonId.Difficulty), _button.Id.Name));
        }
    }
}
