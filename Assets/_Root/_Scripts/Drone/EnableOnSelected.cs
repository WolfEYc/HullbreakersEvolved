using UnityEngine;

namespace Hullbreakers
{
    public class EnableOnSelected : MonoBehaviour
    {
        [SerializeField] Draggable draggable;

        void Awake()
        {
            draggable.Selected += DraggableOnSelected;
            draggable.Deselected += DraggableOnDeselected;
            gameObject.SetActive(ReferenceEquals(SelectionManager.instance.Selected, draggable));
        }

        void DraggableOnDeselected()
        {
            gameObject.SetActive(false);
        }

        void DraggableOnSelected()
        {
            gameObject.SetActive(true);
        }
        
    }
}
