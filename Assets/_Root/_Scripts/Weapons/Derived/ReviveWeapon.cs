using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    public class ReviveWeapon : Weapon
    {
        List<Drone> _drones;

        [SerializeField] IDamageFeedback[] _reviveFeedback;
        
        protected override void Awake()
        {
            base.Awake();
            _drones = DroneManager.instance.Drones;
        }

        public override void Shoot()
        {
            var revives = mods.count.value;
            
            for (int i = 0; i < _drones.Count && revives > 0; i++)
            {
                if (_drones[i].Gameobject.activeSelf) continue;
                _drones[i].joint.TeleportToTarget();
                _drones[i].Gameobject.SetActive(true);
                _drones[i].playerControl.SetPlayerControl();
                _drones[i].damageRefs.hp.SetHp(mods.damage.value);
                
                var pos = _drones[i].transform.position;
                var dir = pos - Transform.position;
                
                
                DeployFeedback(pos, dir);
                revives--;
            }
        }
        
        void DeployFeedback(Vector2 pos, Vector2 dir)
        {
            for (int i = 0; i < _reviveFeedback.Length; i++)
            {
                _reviveFeedback[i].DeployFeedback(999f, pos, dir);
            }
        }
    }
}
