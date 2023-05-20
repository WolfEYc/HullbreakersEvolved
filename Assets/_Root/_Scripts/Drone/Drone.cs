using UnityEngine;

namespace Hullbreakers
{
    public class Drone : MonoBehaviour
    {
        [field: SerializeField] public DroneJoint joint { get; private set; }
        [field: SerializeField] public SimpleDamageRefs damageRefs { get; private set; }
        [field: SerializeField] public DraggableDrone draggable { get; private set; }
        [field: SerializeField] public PlayerControl playerControl { get; private set; }
        [field: SerializeField] public Upgradeable upgradeable { get; private set; }

        IActionOnDestroyed[] _actionsOnDestroyed;
        public GameObject Gameobject { get; private set; }

        void Awake()
        {
            Gameobject = gameObject;
            _actionsOnDestroyed = GetComponentsInChildren<IActionOnDestroyed>();
            damageRefs.hp.OnKilled += OnKilled;
        }

        public void SetInGame()
        {
            draggable.enabled = false;
            joint.RefreshOffsets();
            playerControl.RefreshControllables();
            playerControl.SetPlayerControl();
        }

        public void SetInMenu()
        {
            joint.TeleportToTarget();
            draggable.enabled = true;
            Gameobject.SetActive(true);
        }

        public void Destroy()
        {
            damageRefs.hp.Kill();
            
            Destroy(Gameobject);
        }

        void OnKilled()
        {
            foreach (IActionOnDestroyed actionOnDestroyed in _actionsOnDestroyed)
            {
                actionOnDestroyed.OnDestroyed();
            }
        }

        public void Sell()
        {
            PlayerOrbs.instance.currency.cash += upgradeable.sellbackValue;
            DroneManager.instance.RemoveDrone(this);
        }
    }
}
