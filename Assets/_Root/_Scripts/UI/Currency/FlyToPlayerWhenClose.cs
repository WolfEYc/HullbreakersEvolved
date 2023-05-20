using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class FlyToPlayerWhenClose : MonoBehaviour, IActionOnDestroyed
    {
        public Rigidbody2D rb;
        bool _added;
        
        void OnTriggerEnter2D(Collider2D col)
        {
            if(_added) return;
            
            _added = true;
            FlyToPlayerSystem.instance.Rbs.Add(rb);
        }

        public void OnDestroyed()
        {
            if (!_added) return;
            
            FlyToPlayerSystem.instance.Rbs.Remove(rb);
            _added = false;
        }
    }
}
