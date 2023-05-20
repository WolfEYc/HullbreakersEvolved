using UnityEngine;

namespace Hullbreakers
{
    public class EndGame : MonoBehaviour
    {
        public void EndTheGameFromUILikeAPussy()
        {
            GameStateManager.instance.EndGame();
        }
    }
}
