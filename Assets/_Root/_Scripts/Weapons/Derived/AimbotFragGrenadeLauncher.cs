namespace Hullbreakers
{
	public class AimbotFragGrenadeLauncher : ProjectileWeapon<AimbotFragGrenade>, IBlastRadius, IAimbotWeapon
	{
		ExplosionMods _explosionMods;
		AimbotMods _aimbotMods;
        
		protected override void Awake()
		{
			base.Awake();
			_explosionMods = GetComponentInParent<ExplosionMods>();
			_aimbotMods = GetComponentInParent<AimbotMods>();
		}

		public ExplosionMods explosionMods => _explosionMods;
		public AimbotMods aimbotMods => _aimbotMods;
	}
}