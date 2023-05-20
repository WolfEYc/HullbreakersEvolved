using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public class SystemsManager : PersistentSingleton<SystemsManager>
    {
        [Button]
        public void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
