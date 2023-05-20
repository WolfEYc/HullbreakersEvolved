using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PooledEnemy<T> : PooledObject<T>, IActionOnDestroyed where T : PooledEnemy<T>
    {
        Rigidbody2D _rb;
        [SerializeField] CircleCollider2D circleCollider;

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponent<Rigidbody2D>();
        }

        public override void Spawn(Vector2 pos, float rot, float initialForce)
        {
            base.Spawn(pos, rot, initialForce);
            _rb.velocity = rot.DirectionFromAngleDegrees() * initialForce;
            EnemiesManager.instance.totalEnemies++;
        }

        public override float sizeRadius => circleCollider.radius;
        public void OnDestroyed()
        {
            EnemiesManager.instance.totalEnemies--;
        }
    }
}
