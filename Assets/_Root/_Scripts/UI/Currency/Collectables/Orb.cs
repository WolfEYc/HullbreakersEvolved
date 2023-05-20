namespace Hullbreakers
{
    public class Orb : Collectable<Orb>
    {
        protected override ICurrencyProvider currencyProvider => PlayerOrbs.instance;
    }
}
