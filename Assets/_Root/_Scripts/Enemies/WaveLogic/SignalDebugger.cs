using Doozy.Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    public class SignalDebugger : SerializedMonoBehaviour
    {
        [SerializeField] SignalSender signalToDebug;

        [Button]
        public void SendSignal()
        {
            signalToDebug.SendSignal();
        }
    }
}
