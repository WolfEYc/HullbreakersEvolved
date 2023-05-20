using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Animator))]
    public class PlayerLocator : MonoBehaviour
    {
        Animator _animator;
        static readonly int Locate1 = Animator.StringToHash("Locate");

        void Awake()
        {
            _animator = GetComponent<Animator>();
            GetComponentInParent<ScreenPort>().OnTp += Locate;
        }
        
        public void Locate()
        {
            _animator.SetTrigger(Locate1);
        }
    }
}
