namespace Hullbreakers
{
    public class OrbDisplay : WalletDisplay
    {
        protected override ICurrencyProvider provider => PlayerOrbs.instance;
    }
}
