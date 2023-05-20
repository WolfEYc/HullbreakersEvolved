using UnityEngine;

namespace Hullbreakers
{
    public class PlayerControl : MonoBehaviour, IActionOnDestroyed
    {
        IPlayerControllable[] _controllables;

        void Awake()
        {
            RefreshControllables();
        }
        
        public void SetPlayerControl()
        {
            foreach (IPlayerControllable controllable in _controllables)
            {
                controllable.SetPlayerControl();
            }
        }

        public void RemovePlayerControl()
        {
            foreach (IPlayerControllable controllable in _controllables)
            {
                controllable.RemovePlayerControl();
            }
        }

        public void OnDestroyed()
        {
            RemovePlayerControl();
        }

        public void RefreshControllables()
        {
            _controllables = GetComponentsInChildren<IPlayerControllable>();
        }
    }
}
