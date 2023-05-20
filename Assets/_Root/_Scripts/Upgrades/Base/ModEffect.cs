using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public abstract class ModEffect
    {
        public abstract void Do(GameObject context);

        public abstract void Undo(GameObject context);
    }
}