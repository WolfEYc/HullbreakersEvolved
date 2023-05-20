using System;

namespace Hullbreakers
{
    public class SelectionManager : Singleton<SelectionManager>
    {
        public ISelectable Selected;

        public event Action OnSelected;

        public void Select(ISelectable toSelect)
        {
            Selected?.Deselect();

            Selected = toSelect;
            Selected.Select();
            OnSelected?.Invoke();
        }

        public void Clear()
        {
            Selected?.Deselect();
            Selected = null;
        }
    }
}
