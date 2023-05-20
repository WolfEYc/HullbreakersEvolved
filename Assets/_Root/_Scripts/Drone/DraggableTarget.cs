using UnityEngine;

namespace Hullbreakers
{
    public class DraggableTarget : Draggable
    {
        [SerializeField] Transform rootTransform;
        [SerializeField] PlacementRadius dronePlacementRadius;

        protected override PlacementRadius mustBeInRangeOfthis => dronePlacementRadius;

        const float k_TargetRadius = 2f;
        float _prevTargetRadius;

        Transform _targetTransformParent;

        Vector2 offset => Transform.position - rootTransform.position;

        protected override void SetPrevLocation()
        {
            base.SetPrevLocation();
            _prevTargetRadius = mustBeInRangeOfthis.radius.BaseValue;
            mustBeInRangeOfthis.radius.BaseValue = k_TargetRadius;
            _targetTransformParent = Transform.parent;
            Transform.parent = null;
        }

        public override void Place()
        {
            base.Place();
            rootTransform.rotation =
                Quaternion.AngleAxis(
                    offset.AngleFromDirection(),
                    Vector3.forward);
            Transform.parent = _targetTransformParent;
            mustBeInRangeOfthis.radius.BaseValue = _prevTargetRadius;
        }
    }
}
