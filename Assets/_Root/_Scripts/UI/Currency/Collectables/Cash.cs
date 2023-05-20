namespace Hullbreakers
{
    public class Cash : Collectable<Cash>
    {
        protected override ICurrencyProvider currencyProvider => PlayerCash.instance;
    }
}
