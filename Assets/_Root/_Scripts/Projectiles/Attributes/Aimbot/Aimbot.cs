using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Aimbot : MonoBehaviour, IActionOnDestroyed, ILaunchable, IDamager
    {
        Transform _targetTransform;
        SpriteRenderer _spriteRenderer;
        
        public Health target { get; private set; }
        AimbotSystem _aimbotSystem;
        
        AimbotMods _aimbotMods;
        
        public Rigidbody2D rb { get; private set; }
        
        public readonly Toggleable HasTarget = new();

        Vector2 dir2Target => (Vector2)_targetTransform.position - rb.position;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            /*
            HasTarget.TurnedOn += HasTargetOnTurnedOn;
            HasTarget.TurnedOff += HasTargetOnTurnedOff;
            */
        }
        
/*      Debugging
        void HasTargetOnTurnedOff()
        {
            _spriteRenderer.color = Color.white;
        }

        void HasTargetOnTurnedOn()
        {
            _spriteRenderer.color = Color.red;
        }
*/
        public void OnDestroyed()
        {
            if (!HasTarget.state) return;
            
            target.OnKilled -= AquireNewTarget;
            HasTarget.state = false;
            AimbotSystems.instance.Aimbots.Remove(this);
        }

        void AquireNewTarget()
        {
            _aimbotSystem.AssignTarget(this);
        }
        
        public void AssignTarget(Health newTarget)
        {
            if (HasTarget.state)
            {
                target.OnKilled -= AquireNewTarget;
            }
            else
            {
                HasTarget.state = true;
                AimbotSystems.instance.Aimbots.Add(this);
            }
            
            target = newTarget;
            _targetTransform = target.transform;
            target.OnKilled += AquireNewTarget;
        }
        
        public void PhysicsUpdate()
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + dir2Target.normalized * (_aimbotMods.accel.value * Time.fixedDeltaTime), _aimbotMods.maxvel.value);
        }

        public void Launch(Weapon weapon, float angle)
        {
            _aimbotMods = ((IAimbotWeapon)weapon).aimbotMods;
            
            _aimbotSystem = this.AmPlayerAttack()
                ? AimbotSystems.instance.aimbotForFriendly
                : AimbotSystems.instance.aimbotForEnemy;
            
            AquireNewTarget();
        }

        public int InflictDamage(IDamageable damageable, Vector2 pos, Vector2 dir)
        {
            AquireNewTarget();
            return 0;
        }
        
    }
}

