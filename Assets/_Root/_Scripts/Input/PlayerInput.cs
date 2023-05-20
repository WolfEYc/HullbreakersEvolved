using System;
using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hullbreakers
{
    public class PlayerInput : SerializedMonoBehaviour
    {
        [field:SerializeField] public Toggleable thrusting { get; private set; }
        [field:SerializeField] public Toggleable shooting { get; private set; }

        public event Action ActiveAbilityUsed, UltimateAbilityUsed;
        public event Action TargetUpdated;

        [SerializeField] SignalSender escSender;
        [SerializeField] SignalSender anySender;
        
        public Vector2 target { get; private set; }

        public void Thrust(InputAction.CallbackContext context)
        {
            thrusting.state = context.performed && context.action.IsPressed();
        }

        public void Shoot(InputAction.CallbackContext context)
        {
            shooting.state = context.performed && context.action.IsPressed();
        }

        public void ActiveAbility(InputAction.CallbackContext context)
        {
            if(!context.performed || !context.action.IsPressed()) return;

            ActiveAbilityUsed?.Invoke();
        }

        public void UltimateAbility(InputAction.CallbackContext context)
        {
            if(!context.performed || !context.action.IsPressed()) return;

            UltimateAbilityUsed?.Invoke();
        }

        public void SetTarget(InputAction.CallbackContext context)
        {
            target = MainCam.instance.cam.ScreenToWorldPoint(context.ReadValue<Vector2>());
            TargetUpdated?.Invoke();
        }

        public void Esc(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                escSender.SendSignal();
            }
        }
        
        public void Any(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                anySender.SendSignal();
            }
        }
    }
}
