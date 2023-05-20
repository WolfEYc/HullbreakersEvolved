using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Weapon))]
    public class Recursive : MonoBehaviour, IActionOnDestroyed
    {
        Weapon _weapon;

        void Awake()
        {
            _weapon = GetComponent<Weapon>();
        }

        public void OnDestroyed()
        {
            _weapon.Shoot();
        }
    }
}
