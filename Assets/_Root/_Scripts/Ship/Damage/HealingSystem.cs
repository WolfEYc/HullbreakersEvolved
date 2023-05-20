using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hullbreakers
{
    public class HealingSystem : MonoBehaviour
    {
        public List<Health> toHeal { get; private set; }
        public List<Health> cpy { get; private set; }

        void Awake()
        {
            toHeal = new List<Health>();
            cpy = new List<Health>();
        }

        public int DistributeHeals(int healing)
        {
            cpy.Clear();
            int healedTargets = 0;

            var query = toHeal.OrderBy(health => health.hp);
            
            foreach (Health health in query)
            {
                if(healing <= 0) return healedTargets;
                
                int heals = (int)health.Heal(healing);
                healing -= heals;

                if (heals <= 0) continue;

                healedTargets++;
                cpy.Add(health);
            }

            return healedTargets;
        }
        
    }
}
