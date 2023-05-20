using System.Collections;
using UnityEngine;

namespace Hullbreakers
{
    public class BasicPortalFX : PortalFX<BasicPortalFX>
    {
        const float k_RotPerSpawn = 15f;
        
        protected override IEnumerator SpawnEncounter(Encounter encounter)
        {
            float rot = 0f;

            WaitForSeconds spawnPerSecond = new WaitForSeconds(spawnTime / encounter.totalUnits);
            
            
            for (int i = 0; i < encounter.UnitsToSpawn.Count; i++)
            {
                for (int j = 0; j < encounter.UnitsToSpawn[i].Item2; j++)
                {
                    yield return spawnPerSecond;
                    
                    if(GameStateManager.instance.state == GameStateManager.GameState.Standby) yield break;
                    
                    encounter.UnitsToSpawn[i].Item1.Instantiate(
                        Transform.position,
                        rot,
                        spawnForce);

                    rot += k_RotPerSpawn;
                }
            }
        }
    }
}
