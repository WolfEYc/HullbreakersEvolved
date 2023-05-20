using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class AimbotSystem : MonoBehaviour
    {
        public readonly List<Health> Enemies = new();

        public void AddTarget(Health target)
        {
            Enemies.Add(target);
            target.OnKilled += () => Enemies.Remove(target);
        }

        public void AssignTarget(Aimbot aimbot)
        {
            if (Enemies.Count == 0)
            {
                aimbot.OnDestroyed();
                return;
            }

            if (!TryGetClosestEnemy(aimbot, out Health target))
            {
                aimbot.OnDestroyed();
                return;
            }
            
            aimbot.AssignTarget(target);
        }

        bool TryGetClosestEnemy(Aimbot aimbot, out Health target)
        {
            if (aimbot.HasTarget.state)
            {
                var foundExclusive = TryGetClosestEnemyExclusive(aimbot, out Health exclusiveTarget);
                target = exclusiveTarget;
                return foundExclusive;
            }
            
            Health closest = null;
            float minDist = float.MaxValue;
            bool found = false;
            
            for (int i = 0; i < Enemies.Count; i++)
            {
                float dist = Vector2.Distance(aimbot.rb.position, Enemies[i].transform.position);

                if (!(dist < minDist)) continue;
                
                minDist = dist;
                closest = Enemies[i];
                found = true;
            }

            target = closest;

            return found;
        }

        bool TryGetClosestEnemyExclusive(Aimbot aimbot, out Health target)
        {
            Health closest = null;
            float minDist = float.MaxValue;
            bool found = false;
            
            for (int i = 0; i < Enemies.Count; i++)
            {
                if(Enemies[i] == aimbot.target) continue;
                
                float dist = Vector2.Distance(aimbot.rb.position, Enemies[i].transform.position);

                if (!(dist < minDist)) continue;
                
                minDist = dist;
                closest = Enemies[i];
                found = true;
            }

            target = closest;

            return found;
        }
        
    }
}
