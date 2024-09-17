using Agate.MVC.Base;
using ProjectTA.Module.Enemy;
using UnityEngine;

namespace ProjectTA.Module.EnemyManager
{
    public class EnemyManagerView : BaseView
    {
        public Transform player;
        public GameObject enemyPrefab; // Reference to the enemy prefab
        public float spawnDistance = 10f; // Distance from the camera where enemies should spawn
        public float spawnInterval = 5f; // Interval in seconds between spawns
        private Camera mainCamera;

        void Start()
        {
            mainCamera = Camera.main;
            InvokeRepeating(nameof(SpawnEnemyOutsideCamera), spawnInterval, spawnInterval);
        }

        void SpawnEnemyOutsideCamera()
        {
            // Get the camera bounds in world space
            Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

            float minX = screenBottomLeft.x - spawnDistance;
            float maxX = screenTopRight.x + spawnDistance;
            float minZ = screenBottomLeft.z - spawnDistance;
            float maxZ = screenTopRight.z + spawnDistance;

            Vector3 spawnPosition = GetRandomSpawnPosition(minX, maxX, minZ, maxZ);

            // Instantiate the enemy at the spawn position
            GameObject obj = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            obj.GetComponent<EnemyView>().player = player;
        }

        Vector3 GetRandomSpawnPosition(float minX, float maxX, float minZ, float maxZ)
        {
            Vector3 spawnPosition;

            // Randomly choose to spawn on one of the four edges outside the camera
            int side = Random.Range(0, 4);

            switch (side)
            {
                case 0: // Left side
                    spawnPosition = new Vector3(minX, 0, Random.Range(minZ, maxZ));
                    break;
                case 1: // Right side
                    spawnPosition = new Vector3(maxX, 0, Random.Range(minZ, maxZ));
                    break;
                case 2: // Top side
                    spawnPosition = new Vector3(Random.Range(minX, maxX), 0, maxZ);
                    break;
                case 3: // Bottom side
                    spawnPosition = new Vector3(Random.Range(minX, maxX), 0, minZ);
                    break;
                default:
                    spawnPosition = Vector3.zero;
                    break;
            }

            return spawnPosition;
        }
    }
}