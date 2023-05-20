namespace Hullbreakers
{
    public class TorpedoWeapon : ProjectileWeapon<Torpedo>, IAimbotWeapon
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
