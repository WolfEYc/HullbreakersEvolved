using UnityEngine;
using UnityEngine.EventSystems;

namespace Hullbreakers
{
    public class Selectable : MonoBehaviour, ISelectable, IPointerDownHandler
    {
        [SerializeField] PlacementRadius radiusIndicator;
        
        public void Select()
        {
            radiusIndicator.Toggle(true);
        }

        public void Deselect()
        {
            radiusIndicator.Toggle(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectionManager.instance.Select(this);
        }
    }
}
