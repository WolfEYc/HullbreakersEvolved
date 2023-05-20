using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public class PlayerRerolls : Singleton<PlayerRerolls>, ICurrencyProvider
    {
        Currency _rerolls;
        public Currency currency => _rerolls;

        [SerializeField] SignalSender rerollSender;

        protected override void Awake()
        {
            base.Awake();
            _rerolls = new("rerolls", false);
            
        }

        public void UseReroll()
        {
            if (_rerolls.cash < 1)
            {
                return;
            }

            _rerolls.cash--;
            rerollSender.SendSignal();
        }

        [Button]
        public void AddReroll()
        {
            _rerolls.cash++;
        }
    }
}
