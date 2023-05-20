namespace Hullbreakers
{
    public class Reroll : Collectable<Reroll>
    {
        protected override ICurrencyProvider currencyProvider => PlayerRerolls.instance;
    }
}
