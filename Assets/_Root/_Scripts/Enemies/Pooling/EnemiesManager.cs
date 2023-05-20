using System;

namespace Hullbreakers
{
    public class EnemiesManager : Singleton<EnemiesManager>
    {
        public event Action EnemiesReachedZero;
        
        int _totalEnemies;
        public int totalEnemies
        {
            get => _totalEnemies;
            set
            {
                if(_totalEnemies == value) return;
                
                _totalEnemies = value;
                if (_totalEnemies == 0)
                {
                    EnemiesReachedZero?.Invoke();
                }
            }
        }
    }
}
