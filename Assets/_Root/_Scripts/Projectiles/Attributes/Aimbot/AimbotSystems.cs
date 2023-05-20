using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class AimbotSystems : Singleton<AimbotSystems>
    {
        [HideInInspector] public readonly List<Aimbot> Aimbots = new();
        
        [field: SerializeField] public AimbotSystem aimbotForFriendly { get; private set; }
        [field: SerializeField] public AimbotSystem aimbotForEnemy { get; private set; }
        
        public void AddHealth(Health health)
        {
            if(health.AmPlayer())
            {
                aimbotForEnemy.AddTarget(health);
            }
            else
            {
                aimbotForFriendly.AddTarget(health);
            }
        }
        
        
        void FixedUpdate()
        {
            for (int i = 0; i < Aimbots.Count; i++)
            {
                Aimbots[i].PhysicsUpdate();
            }
        }
    }
}
