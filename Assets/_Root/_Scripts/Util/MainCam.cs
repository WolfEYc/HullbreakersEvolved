using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(Camera))]
    public class MainCam : Singleton<MainCam>
    {
        public Camera cam { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            cam = GetComponent<Camera>();
        }

        public Vector2 bottomLeft => cam.ScreenToWorldPoint(Vector3.zero);
        public Vector2 topRight => cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        
        float xInRange => Random.Range(bottomLeft.x, topRight.x);
        float yInRange => Random.Range(bottomLeft.y, topRight.y);
        
        const float k_SpawnDist = 2f;

        public Vector2 OffCameraSpawnLocation(int spawnDir, float sizeRadius)
        {
            Vector2 location = default;
            float spawnDist = k_SpawnDist + sizeRadius;
            
            switch (spawnDir)
            {
                case 0:
                    location.x = xInRange;
                    location.y = bottomLeft.y - spawnDist;
                    break;
                case 1:
                    location.x = topRight.x + spawnDist;
                    location.y = yInRange;
                    break;
                case 2:
                    location.x = xInRange;
                    location.y = topRight.y + spawnDist;
                    break;
                case 3:
                    location.x = bottomLeft.x - spawnDist;
                    location.y = yInRange;
                    break;
            }

            return location;
        }

        public Vector2 onCameraSpawnLocation => 
            new (Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));

    }
}
