using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.Enemy;
using UnityEngine;

namespace ProjectTA.Module.EnemyManager
{
    public class EnemyManagerController : ObjectController<EnemyManagerController, EnemyManagerView>
    {
        public override void SetView(EnemyManagerView view)
        {
            base.SetView(view);
            view.SetCallback(OnSpawnEnemy);
        }

        private void OnSpawnEnemy()
        {
            // Get the camera bounds in world space
            Vector3 screenBottomLeft = _view.mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _view.mainCamera.nearClipPlane));
            Vector3 screenTopRight = _view.mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _view.mainCamera.nearClipPlane));

            float minX = screenBottomLeft.x - _view.spawnDistance;
            float maxX = screenTopRight.x + _view.spawnDistance;
            float minZ = screenBottomLeft.z - _view.spawnDistance;
            float maxZ = screenTopRight.z + _view.spawnDistance;

            Vector3 spawnPosition = GetRandomSpawnPosition(minX, maxX, minZ, maxZ);

            // Instantiate the enemy at the spawn position
            GameObject obj = GameObject.Instantiate(_view.enemyPrefab, spawnPosition, Quaternion.identity);
            EnemyView enemyView = obj.GetComponent<EnemyView>();
            EnemyController enemyController = new EnemyController();
            InjectDependencies(enemyController);
            enemyController.Init(enemyView);
            enemyView.player = _view.player;
            
            obj.name = $"Enemy_{_view.enemyCount}";
            _view.enemyCount++;
        }

        private Vector3 GetRandomSpawnPosition(float minX, float maxX, float minZ, float maxZ)
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