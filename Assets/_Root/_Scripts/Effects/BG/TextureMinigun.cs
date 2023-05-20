using UnityEngine;

namespace Hullbreakers
{
    public class TextureMinigun : ProjectileWeapon<TextureProjectile>
    {
        public Sprite[] textures;

        [SerializeField] Vector2 rotMinMax;
        float _defaultz;

        protected override void Awake()
        {
            base.Awake();
            _defaultz = transform.rotation.eulerAngles.z;
        }

        protected override void Start()
        {
            base.Start();
            StartShooting();
        }

        public override void Shoot()
        {
            Transform.rotation = Quaternion.AngleAxis(
                _defaultz + Random.Range(rotMinMax.x, rotMinMax.y),
                Vector3.forward);
            base.Shoot();
        }
    }
}
