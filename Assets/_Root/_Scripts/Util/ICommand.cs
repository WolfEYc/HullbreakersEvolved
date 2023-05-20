namespace Hullbreakers
{
    public interface ICommand
    {
        public void Do();
        public void Undo();
    }
}
