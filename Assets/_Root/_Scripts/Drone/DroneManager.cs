using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class DroneManager : Singleton<DroneManager>
    {
        public readonly List<Drone> Drones = new();
        [SerializeField] Transform droneSpawnTransform;
        
        void Start()
        {
            PlayerManager.instance.OnPlayerPinned += OnWaveEnd;
            WaveManager.instance.WaveStarted += OnWaveStarted;
            GameStateManager.instance.GameEnded += OnGameEnded;
        }

        public Drone SpawnDrone(GameObject prefab, Vector3 pos)
        {
            Drone drone = Instantiate(prefab, pos, Quaternion.AngleAxis(90f, Vector3.forward), droneSpawnTransform)
                .GetComponent<Drone>();
            
            Drones.Add(drone);
            
            return drone;
        }

        public void RemoveDrone(Drone drone)
        {
            Drones.Remove(drone);
            drone.Destroy();
        }

        public void RemovePlayerControl()
        {
            foreach (Drone drone in Drones)
            {
                drone.playerControl.RemovePlayerControl();
            }
        }

        void OnWaveEnd()
        {
            foreach (Drone drone in Drones)
            {
                drone.SetInMenu();
            }
        }
        
        void OnWaveStarted()
        {
            foreach (Drone drone in Drones)
            {
                drone.SetInGame();
            }
        }
        
        void OnGameEnded()
        {
            foreach (Drone drone in Drones)
            {
                drone.Destroy();
            }
            
            Drones.Clear();
        }

        void FixedUpdate()
        {
            if(WaveManager.instance.currentWaveState == WaveManager.WaveState.Standby) return;
            
            foreach (Drone drone in Drones)
            {
                drone.joint.PhysicsUpdate();
            }
        }
    }
}
