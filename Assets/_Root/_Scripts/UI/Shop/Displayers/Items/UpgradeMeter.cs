
using UnityEngine;
using UnityEngine.UI;

namespace Hullbreakers
{
    public class UpgradeMeter : MonoBehaviour
    {
        [SerializeField] Image[] upgradeSquares;
        
        static readonly Color MeterFillEmpty = new(0f, 0f, 0f, 0.5f);
        
        public void FillMeter(int squares)
        {
            int i;
            for (i = 0; i < squares; i++)
            {
                upgradeSquares[i].color = Color.green;
            }

            for (; i < upgradeSquares.Length; i++)
            {
                upgradeSquares[i].color = MeterFillEmpty;
            }
        }
    }
}
