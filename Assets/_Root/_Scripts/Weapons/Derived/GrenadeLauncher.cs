namespace Hullbreakers
{
    public class GrenadeLauncher : ProjectileWeapon<Grenade>, IBlastRadius
    {
        ExplosionMods _explosionMods;
        
        protected override void Awake()
        {
            base.Awake();
            _explosionMods = GetComponentInParent<ExplosionMods>();
        }

        public ExplosionMods explosionMods => _explosionMods;
    }
}
