using System;
using UnityEngine;

namespace Hullbreakers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
	 public GameObject playerInstance { get; private set; }
	 public Rigidbody2D playerRb { get; private set; }
         public SimpleDamageRefs playerHealth { get; private set; }
         public PlayerAbilities playerAbilities { get; private set; }
         public PlayerInput playerInput { get; private set; }
         public PlayerControl playerShipControl { get; private set; }
         public LocationPin playerLocationPinner { get; private set; }
         public PlacementRadius playerPlacementRadius { get; private set; }
         public Transform droneTargets { get; private set; }

         public GameObject playerPrefab;

         public event Action OnNewShip, OnPlayerPinned;

         static readonly Vector2 SpawnForce = new(10f, 10f);

         protected override void Awake()
         {
	         base.Awake();
	         playerInput = GetComponent<PlayerInput>();
         }

         void Start()
         {
	         GameStateManager.instance.GameStarted += SpawnPlayer;
	         GameStateManager.instance.GameEnded += KillPlayer;
         }

         void KillPlayer()
         {
	         playerHealth.hp.Kill();
         }

         public void SpawnPlayer()
         {
	         if (playerRb != null)
	         {
		         SpawnPlayer(playerRb.position, playerRb.rotation);
	         }
	         else
	         {
		         SpawnPlayerFresh();
	         }
         }

         public void PinPlayer(Vector2 location)
         {
	         playerLocationPinner.Pin(location);
	         playerHealth.hp.Resurrect();
	         playerHealth.shield.Resurrect();
	         
	         
	         OnPlayerPinned?.Invoke();
         }

         public void UnPinPlayer()
         {
	         playerLocationPinner.Unpin();
         }
         
         void SpawnPlayer(Vector2 position, float angle)
         {
	         playerInstance = Instantiate(
		         playerPrefab,
		         position,
		         Quaternion.AngleAxis(angle, Vector3.forward));
	         
	         AssociatePlayer();
         }
         
         void SpawnPlayerFresh()
         {
	         SpawnPlayer(MainCam.instance.bottomLeft, 45f);
	         playerRb.velocity = SpawnForce;
         }

         void AssociatePlayer()
         {
	         playerRb = playerInstance.GetComponent<Rigidbody2D>();
	         playerHealth = playerInstance.GetComponent<SimpleDamageRefs>();
	         playerShipControl = playerInstance.GetComponent<PlayerControl>();
	         playerAbilities = playerInstance.GetComponent<PlayerAbilities>();
	         playerLocationPinner = playerInstance.GetComponentInChildren<LocationPin>();
	         playerPlacementRadius = playerInstance.GetComponentInChildren<PlacementRadius>(true);
	         droneTargets = playerInstance.transform.Find("DroneTargets");
	         playerShipControl.SetPlayerControl();
	         OnNewShip?.Invoke();
	         playerHealth.hp.OnKilled += GameStateManager.instance.EndGame;
         }

         
    }
}
