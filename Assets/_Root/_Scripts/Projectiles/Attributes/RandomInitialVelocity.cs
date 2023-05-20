using UnityEngine;
using Random = UnityEngine.Random;

namespace Hullbreakers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RandomInitialVelocity : MonoBehaviour
    {
        Rigidbody2D _rigidbody;
        public float power;

        [SerializeField] float minAngular;
        [SerializeField] float maxAngular;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void OnEnable()
        {
            _rigidbody.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(1f, power);


            if (maxAngular > 0f)
            {
                _rigidbody.angularVelocity = Random.Range(minAngular, maxAngular);
            }
        }
    }
}
