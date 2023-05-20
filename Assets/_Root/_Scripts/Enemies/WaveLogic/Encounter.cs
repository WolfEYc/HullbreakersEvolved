using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Hullbreakers
{
    [CreateAssetMenu(order = 0, fileName = "newEncounter", menuName = "Waves/Encounter")]
    public class Encounter : SerializedScriptableObject
    {
        public List<Tuple<ISpawnable, int>> UnitsToSpawn;
        
        public Spawner.SpawnPattern[] spawnPatterns;

        public float delayForNextEncounter;

        int _totalUnitsCached;
        public int totalUnits
        {
            get
            {
                if (_totalUnitsCached == 0)
                {
                    CacheTotalUnits();
                }

                return _totalUnitsCached;
            }
        }

        void CacheTotalUnits()
        {
            for (int i = 0; i < UnitsToSpawn.Count; i++)
            {
                _totalUnitsCached += UnitsToSpawn[i].Item2;
            }
        }
    }
}
