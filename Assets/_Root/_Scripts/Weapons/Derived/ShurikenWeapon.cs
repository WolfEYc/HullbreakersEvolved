namespace Hullbreakers
{
    public class ShurikenWeapon : ProjectileWeapon<Shuriken>, IAimbotWeapon
    {
        AimbotMods _aimbotMods;
        protected override void Awake()
        {
            base.Awake();
            _aimbotMods = GetComponentInParent<AimbotMods>();
        }
        
        public AimbotMods aimbotMods => _aimbotMods;
    }
}
