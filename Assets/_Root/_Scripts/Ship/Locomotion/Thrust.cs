using System;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PhysicsMods))]
    public class Thrust : MonoBehaviour, IPlayerControllable
    {
        PhysicsMods _physicsMods;
        Rigidbody2D _rb;
        Transform _transform;
        LocalTimeScale _localTimeScale;
        
        public event Action StartedThrusting;
        public event Action StopThrusting;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _localTimeScale = GetComponentInParent<LocalTimeScale>();
            _physicsMods = GetComponent<PhysicsMods>();
            _transform = transform;
            BindFriction();
        }

        void BindFriction()
        {
            Friction friction = GetComponent<Friction>();
            
            if(friction == null) return;

            StartedThrusting += friction.ToggleOff;
            StopThrusting += friction.ToggleOn;
        }

        void FixedUpdate()
        {
            _rb.velocity = Vector2.ClampMagnitude(
                _rb.velocity + (Vector2)_transform.right * (_physicsMods.accel.value * Time.fixedDeltaTime * _localTimeScale.timeScale.value),
                _physicsMods.maxVel.value);
        }

        public void SetPlayerControl()
        {
            PlayerManager.instance.playerInput.thrusting.TurnedOff += ToggleOff;
            PlayerManager.instance.playerInput.thrusting.TurnedOn += ToggleOn;
        }

        public void RemovePlayerControl()
        {
            ToggleOff();
            PlayerManager.instance.playerInput.thrusting.TurnedOff -= ToggleOff;
            PlayerManager.instance.playerInput.thrusting.TurnedOn -= ToggleOn;
        }

        void ToggleOff()
        {
            enabled = false;
        }

        void ToggleOn()
        {
            enabled = true;
        }

        void OnEnable()
        {
            StartedThrusting?.Invoke();
        }

        void OnDisable()
        {
            StopThrusting?.Invoke();
        }
    }
}
