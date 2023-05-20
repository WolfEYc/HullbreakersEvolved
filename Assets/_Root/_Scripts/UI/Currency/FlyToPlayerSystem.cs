using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class FlyToPlayerSystem : Singleton<FlyToPlayerSystem>
    {
        public readonly List<Rigidbody2D> Rbs = new();

        const float k_Accel = 20f, k_MaxVel = 10f;
        

        protected override void Awake()
        {
            base.Awake();
            GameStateManager.instance.GameStarted += GameStarted;
            GameStateManager.instance.GameEnded += GameEnded;
            enabled = false;
        }

        void GameStarted()
        {
            enabled = true;
        }

        void GameEnded()
        {
            enabled = false;
        }

        void FixedUpdate()
        {
            Vector2 playerPos = PlayerManager.instance.playerRb.position;
            float physicsUpdateAccel = k_Accel * Time.fixedDeltaTime;
            
            for(int i = 0; i < Rbs.Count; i++)
            {
                Vector2 dir = (playerPos - Rbs[i].position).normalized;

                Rbs[i].velocity =
                    Vector2.ClampMagnitude(Rbs[i].velocity + dir * physicsUpdateAccel, k_MaxVel);
            }
        }
    }
}
