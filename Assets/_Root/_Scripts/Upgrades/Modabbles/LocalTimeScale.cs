using UnityEngine;

namespace Hullbreakers
{
    public class LocalTimeScale : MonoBehaviour
    {
        [field: SerializeField] public Multiplyable timeScale { get; private set; }

        void Awake()
        {
            timeScale.RefreshValue();
        }
    }
}
