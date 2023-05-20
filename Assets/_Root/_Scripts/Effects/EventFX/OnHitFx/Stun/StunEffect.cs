using UnityEngine;
using UnityEngine.VFX;

namespace Hullbreakers
{
    [RequireComponent(typeof(VisualEffect))]
    public class StunEffect : MonoBehaviour, IActionOnDestroyed
    {
        [SerializeField] SimpleDamageRefs simpleDamageRefs;
        [SerializeField] Rigidbody2D rb;
        
        LocalTimeScale _localTimeScale;
        VisualEffect _stunEffect;
        float _prevMultiplier = 1f;
        Timer _timer;
        
        void Awake()
        {
            _stunEffect = GetComponent<VisualEffect>();
            _localTimeScale = GetComponentInParent<LocalTimeScale>();
            
            if (simpleDamageRefs.shield != null)
            {
                simpleDamageRefs.shield.OnStunned += Stun;
            }

            simpleDamageRefs.hp.OnStunned += Stun;

            _timer = gameObject.AddComponent<Timer>();
            _timer.Expired += OnDestroyed;
        }

        void Stun(float duration)
        {
            if(simpleDamageRefs.hp.dead) return;
            
            rb.velocity = Vector2.zero;
            _prevMultiplier = _localTimeScale.timeScale.Multiplier;
            _localTimeScale.timeScale.Multiplier = 0f;
            
            _stunEffect.Play();
            _timer.StartCountdown(duration);
        }
        
        public void OnDestroyed()
        {
            _localTimeScale.timeScale.Multiplier = _prevMultiplier;
            _stunEffect.Stop();
        }
    }
}
