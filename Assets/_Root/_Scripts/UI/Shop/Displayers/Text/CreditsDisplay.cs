namespace Hullbreakers
{
    public class CreditsDisplay : WalletDisplay
    {
        protected override ICurrencyProvider provider => PlayerCash.instance;
    }
}
