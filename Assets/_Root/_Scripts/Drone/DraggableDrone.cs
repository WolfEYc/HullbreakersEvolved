namespace Hullbreakers
{
    public class DraggableDrone : Draggable
    {
        protected override PlacementRadius mustBeInRangeOfthis => PlayerManager.instance.playerPlacementRadius;
    }
}
