using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hullbreakers
{
    public abstract class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, ISelectable
    {
        public event Action Selected, Deselected;
        Vector2 _positionBeforeDragging;

        [SerializeField] PlacementRadius placementRadius;
        

        [SerializeField] protected Transform Transform;

        protected abstract PlacementRadius mustBeInRangeOfthis { get; }

        bool _doesNotHavePlacementCollider;

        public bool Placeable { get; private set; } = true;

        void UpdateColors()
        {
            mustBeInRangeOfthis.SetColor(Placeable ? Color.white : Color.red);

            if (!_doesNotHavePlacementCollider)
            {
                placementRadius.SetColor(Placeable ? Color.white : Color.red);
            }
        }
        
        protected virtual void SetPrevLocation()
        {
            _positionBeforeDragging = Transform.position;
        }
        void ReturnToPrevLocation()
        {
            Transform.position = _positionBeforeDragging;
            Placeable = true;
            UpdateColors();
        }
        
        public bool hasBeenPlacedBefore { get; private set; }
        
        public void UpdatePlaceable()
        {
            Vector2 position = PlayerManager.instance.playerInput.target;
            
            Transform.position = new Vector3(position.x, position.y, 1);

            bool inCollision;
            
            if (_doesNotHavePlacementCollider)
            {
                inCollision = false;
            }
            else
            {
                inCollision = !ReferenceEquals(Physics2D.OverlapCircle(position,
                                  placementRadius.radius.value,
                                  ExtensionMethods.PlayerMask,
                                  -1,
                                  0.5f),
                              null);
            }

            bool inRange = Vector2.Distance(
                mustBeInRangeOfthis.position,
                position
            ) < mustBeInRangeOfthis.radius.value;

            bool placeable = !inCollision && inRange;
            
            if(Placeable == placeable) return;
            Placeable = placeable;
            UpdateColors();
        }

        public virtual void Place()
        {
            if (Placeable)
            {
                Transform.position = (Vector2)Transform.position;
                hasBeenPlacedBefore = true;
            }
            else
            {
                ReturnToPrevLocation();
            }
        }

        Coroutine _bindMouseRoutine;
        IEnumerator BindToMouseRoutine()
        {
            while (true)
            {
                UpdatePlaceable();
                yield return null;
            }
        }
        
        
        public void OnPointerDown(PointerEventData eventData)
        {
            SetPrevLocation();
            BindToMouse();
        }

        public void BindToMouse()
        {
            if (!_doesNotHavePlacementCollider)
            {
                SelectionManager.instance.Select(this);
            }
            
            UpdateColors();
            
            if(_bindMouseRoutine != null) return;
            
            mustBeInRangeOfthis.Toggle(true);
            
            _bindMouseRoutine = StartCoroutine(BindToMouseRoutine());
        }

        public void UnbindFromMouse()
        {
            if(_bindMouseRoutine == null) return;
            StopCoroutine(_bindMouseRoutine);
            _bindMouseRoutine = null;
            UpdatePlaceable();

            if (!_doesNotHavePlacementCollider)
            {
                mustBeInRangeOfthis.Toggle(false);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(!enabled) return;

            UnbindFromMouse();

            Place();
        }

        public void Select()
        {
            if (!_doesNotHavePlacementCollider)
            {
                placementRadius.Toggle(true);
            }
            Selected?.Invoke();
        }

        public void Deselect()
        {
            if (!_doesNotHavePlacementCollider)
            {
                placementRadius.Toggle(false);
            }
            Deselected?.Invoke();
        }

        void Awake()
        {
            _doesNotHavePlacementCollider = placementRadius == null;
        }
    }
}
