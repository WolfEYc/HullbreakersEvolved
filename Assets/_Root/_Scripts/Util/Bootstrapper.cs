using UnityEngine;

namespace Hullbreakers
{
    public static class Bootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void SpawnSystems()
        {
            Object.Instantiate(Resources.Load("PersistentSystems"));
        }
    }
}
