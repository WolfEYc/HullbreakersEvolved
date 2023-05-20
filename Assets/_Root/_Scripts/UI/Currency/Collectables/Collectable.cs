using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Collectable<T> : TimedPooledObject<T> where T : Collectable<T>
    {
        [SerializeField] int amount;
        CircleCollider2D _collider2D;

        protected override void Awake()
        {
            base.Awake();
            _collider2D = GetComponent<CircleCollider2D>();
        }

        protected abstract ICurrencyProvider currencyProvider { get; }

        void OnTriggerEnter2D(Collider2D col)
        {
            currencyProvider.currency.cash += amount;
            Release();
        }

        public override float sizeRadius => _collider2D.radius;
    }
}
