using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace Hullbreakers
{
    public static class Spawner
    {
        public enum SpawnPattern
        {
            OffCameraRandomSide,
            BasicPortal,
        }
        
        static int randomSpawnDir => Random.Range(0, 4);

        const float k_SpawnForceOffScreen = 5f;
        
        static readonly List<Action<Encounter>> SpawnPatterns = new()
        {
            OffCameraRandomSide,
            BasicPortal
        };

        public static void Spawn(this Encounter encounter)
        {
            SpawnPatterns[(int)encounter.spawnPatterns.RandomElement()].Invoke(encounter);
        }
        
        static void OffCameraRandomSide(Encounter encounter)
        {
            int spawnDir = randomSpawnDir;
            float spawnAngle = spawnDir * 90f;
            
            for (int i = 0; i < encounter.UnitsToSpawn.Count; i++)
            {
                for (int j = 0; j < encounter.UnitsToSpawn[i].Item2; j++)
                {
                    var spawnable = encounter.UnitsToSpawn[i].Item1;
                    
                    spawnable.Instantiate(
                        MainCam.instance.OffCameraSpawnLocation(spawnDir, spawnable.sizeRadius),
                        spawnAngle,
                        k_SpawnForceOffScreen);
                }
            }
        }
        static void PortalSpawnHelper<T>(Encounter encounter) where T : PortalFX<T>
        {
           GenericPool<T>.instance.Get().Spawn(MainCam.instance.onCameraSpawnLocation, encounter);
        }
        
        static void BasicPortal(Encounter encounter)
        {
            PortalSpawnHelper<BasicPortalFX>(encounter);
        }
        
    }
}
