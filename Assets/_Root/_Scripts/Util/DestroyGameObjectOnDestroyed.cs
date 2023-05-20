using UnityEngine;

namespace Hullbreakers
{
    public class DestroyGameObjectOnDestroyed : MonoBehaviour
    {
        [SerializeField] GameObject toDestroy;

        IActionOnDestroyed _actionsOnDestroyed;
        
        public void OnDestroyed()
        {
            foreach (IActionOnDestroyed actionOnDestroyed in toDestroy.GetComponentsInChildren<IActionOnDestroyed>())
            {
                actionOnDestroyed.OnDestroyed();
            }
            
            Destroy(toDestroy);
        }
    }
}
