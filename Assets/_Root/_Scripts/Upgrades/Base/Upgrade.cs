using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hullbreakers
{
    [CreateAssetMenu(order = 1, fileName = "NewUpgrade", menuName = "Upgrade")]
    public class Upgrade : SerializedScriptableObject
    {
        [field:SerializeField] public int price { get; private set; }
        [field:SerializeField] public Sprite icon { get; private set; }
        [field:SerializeField] public string description { get; private set; }
        
        [SerializeReference] List<ModEffect> effects;

        public void Do(GameObject context)
        {
            foreach (ModEffect effect in effects)
            {
                effect.Do(context);
            }
        }

        public void Undo(GameObject context)
        {
            foreach (ModEffect effect in effects)
            {
                effect.Undo(context);
            }
        }
    }
}
