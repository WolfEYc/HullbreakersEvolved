using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public class PlayerOrbs : Singleton<PlayerOrbs>, ICurrencyProvider
    {
        Currency _orbs;
        
        [Button]
        public void DebugMoney()
        {
            Debug.Log(currency.cash);
        }
        
        [Button]
        void SetMoney(int amt)
        {
            _orbs.cash = amt;
        }
        
        public Currency currency => _orbs;

        protected override void Awake()
        {
            base.Awake();
            _orbs = new("orbs", false);
        }
    }
}
