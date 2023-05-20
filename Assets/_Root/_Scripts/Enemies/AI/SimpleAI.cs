using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Thrust), typeof(Rotation))]
    public class SimpleAI : MonoBehaviour
    {
        [SerializeField] Vector2 thrustThresholds;
        
        Thrust _thrust;
        Rotation _rotation;
        Rigidbody2D _rb;
        static Rigidbody2D playerRb => PlayerManager.instance.playerRb;
        Weapon[] _weapons;

        bool _thrusting;

        float distance2Player => Vector2.Distance(playerRb.position, _rb.position);
        
        void Awake()
        {
            _thrust = GetComponent<Thrust>();
            _rotation = GetComponent<Rotation>();
            _rb = GetComponent<Rigidbody2D>();
            _weapons = GetComponentsInChildren<Weapon>();
            
            GameStateManager.instance.GameEnded += OnGameEnded;
            GameStateManager.instance.GameStarted += OnGameStarted;
            
            if (GameStateManager.instance.state != GameStateManager.GameState.InGame)
            {
                OnGameEnded();
            }
        }

        void OnGameStarted()
        {
            enabled = true;
        }

        void OnGameEnded()
        {
            enabled = false;
        }

        void OnEnable()
        {
            SetThrusting();
        }

        void FixedUpdate()
        {
            _rotation.target = playerRb.position;
            
            CheckThrusting();
        }

        void CheckThrusting()
        {
            bool shouldBeThrusting =
                _thrusting ? distance2Player > thrustThresholds.x : distance2Player > thrustThresholds.y;
            
            if(shouldBeThrusting == _thrusting) return;
            _thrusting = shouldBeThrusting;
            SetThrusting();
        }

        void SetThrusting()
        {
            ToggleWeapons(!_thrusting);
            _thrust.enabled = _thrusting;
        }

        void ToggleWeapons(bool on)
        {
            if (on)
            {
                foreach (Weapon weapon in _weapons)
                {
                    weapon.StartShooting();
                }
            }
            else
            {
                foreach (Weapon weapon in _weapons)
                {
                    weapon.StopShooting();
                }
            }
        }
    }
}
