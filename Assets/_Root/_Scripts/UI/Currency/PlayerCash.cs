using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public class PlayerCash : Singleton<PlayerCash>, ICurrencyProvider
    {
        Currency _hullCredits;
        
        public Currency currency => _hullCredits;

        [Button]
        public void DebugMoney()
        {
            Debug.Log(currency.cash);
        }

        protected override void Awake()
        {
            base.Awake();
            _hullCredits = new("HullCredits", true);
        }

        
        [Button]
        void SetMoney(int amt)
        {
            _hullCredits.cash = amt;
        }

    }
}
