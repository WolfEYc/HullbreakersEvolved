using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(TrailRenderer))]
    public class ClearTrailOnTp : MonoBehaviour
    {
        void Awake()
        {
            GetComponentInParent<ScreenPort>().OnTp += GetComponent<TrailRenderer>().Clear;
        }
    }
}
