using TMPro;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class WalletDisplay : MonoBehaviour
    {
        TMP_Text _text;

        protected abstract ICurrencyProvider provider { get; }

        void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        void Start()
        {
            provider.currency.CashChanged += CurrencyOnCashChanged;
            CurrencyOnCashChanged();
        }

        void CurrencyOnCashChanged()
        {
            _text.SetText(provider.currency.cash.ToString());
        }
    }
}
