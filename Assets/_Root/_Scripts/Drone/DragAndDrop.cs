using UnityEngine;
using UnityEngine.EventSystems;

namespace Hullbreakers
{
    public class DragAndDrop : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler, IPointerDownHandler
    {
        [SerializeField] ItemDisplay itemDisplay;
        
        Draggable _toDrag;
        Drone _draggy;
        
        public void OnEndDrag(PointerEventData eventData)
        {
            if(_toDrag == null) return;
            _toDrag.UnbindFromMouse();
            if (_toDrag.Placeable)
            {
                _toDrag.Place();
                _toDrag = null;
                return;
            }
            
            itemDisplay.InvokeSelect();

            DroneManager.instance.RemoveDrone(_draggy);
            
            itemDisplay.Refund();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            bool affordable = itemDisplay.affordable;
            
            if(!affordable) return;
            
            itemDisplay.Purchase();
            
            _draggy = DroneManager.instance.SpawnDrone(itemDisplay.so.prefab, transform.position);

            _toDrag = _draggy.draggable;
            
            _toDrag.BindToMouse();
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            itemDisplay.InvokeSelect();
        }
    }
}
