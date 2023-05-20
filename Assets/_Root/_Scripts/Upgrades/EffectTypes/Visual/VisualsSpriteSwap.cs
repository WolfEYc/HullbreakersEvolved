using System;
using UnityEngine;

namespace Hullbreakers
{
    [Serializable]
    public class VisualsSpriteSwap : ModEffect
    {
        [SerializeField] Sprite replacement;
        [SerializeField] Color color;
        
        public override void Do(GameObject context)
        {
            context.transform.Find("Visuals").GetComponent<SpriteRenderer>().sprite = replacement;
            context.GetComponent<ColorController>().SetColor(color);
        }

        public override void Undo(GameObject context)
        {
            
        }
    }
}
