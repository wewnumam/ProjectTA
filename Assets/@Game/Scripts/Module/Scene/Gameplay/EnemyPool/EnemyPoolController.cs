using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.Enemy;
using ProjectTA.Module.GameConstants;
using UnityEngine;

namespace ProjectTA.Module.EnemyPool
{
    public class EnemyPoolController : ObjectController<EnemyPoolController, EnemyPoolModel, EnemyPoolView>
    {
        public void SetEnemyPrefab(GameObject enemyPrefab) => _model.SetEnemyPrefab(enemyPrefab);

        public void SetEnemyConstants(EnemyConstants enemyConstants) => _model.SetEnemyConstants(enemyConstants);

        public override void SetView(EnemyPoolView view)
        {
            base.SetView(view);
            SetPoolObject();
            view.SetCallback(OnSpawnEnemy);
        }

        private void OnSpawnEnemy(float deltaTime)
        {
            if (_model.CanSpawn())
            {
                SpawnBullet();
                _model.ResetCountdown();
            }
            _model.DecreaseCountdown(deltaTime);
        }

        private void SetPoolObject()
        {
            for (int i = 0; i < _model.EnemyConstants.EnemyPoolSize; i++)
            {
                _model.AddEnemy(CreateEnemyObject());
            }
        }

        private GameObject CreateEnemyObject()
        {
            // Get the camera bounds in world space
            Vector3 screenBottomLeft = _view.mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _view.mainCamera.nearClipPlane));
            Vector3 screenTopRight = _view.mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _view.mainCamera.nearClipPlane));

            float minX = screenBottomLeft.x - _model.EnemyConstants.EnemySpawnDistance;
            float maxX = screenTopRight.x + _model.EnemyConstants.EnemySpawnDistance;
            float minZ = screenBottomLeft.z - _model.EnemyConstants.EnemySpawnDistance;
            float maxZ = screenTopRight.z + _model.EnemyConstants.EnemySpawnDistance;

            Vector3 spawnPosition = GetRandomSpawnPosition(minX, maxX, minZ, maxZ);

            // Instantiate the enemy at the spawn position
            GameObject enemy = GameObject.Instantiate(_model.EnemyPrefab, spawnPosition, Quaternion.identity);
            enemy.SetActive(false);

            EnemyView enemyView = enemy.GetComponent<EnemyView>();
            if (enemyView == null)
            {
                enemy.AddComponent<EnemyView>();
                enemyView = enemy.GetComponent<EnemyView>();
            }
            EnemyController enemyController = new EnemyController();
            InjectDependencies(enemyController);
            enemyController.Init(enemyView, _view.player, _model.EnemyConstants.EnemyDestroyDelay);

            return enemy;
        }

        private void SpawnBullet()
        {
            GameObject bullet = _model.GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.SetParent(null);
                bullet.SetActive(true);
            }
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

        internal void OnUpdateCountdown(UpdateCountdownMessage message)
        {
            _model.SetIsGameCountdownBelowZero(message.IsCurrentCountdownBelowZero());
        }

        internal void OnGameState(GameStateMessage message)
        {
            _model.SetIsPlaying(message.GameState == Utility.EnumManager.GameState.Playing);
            _model.SetEnemyPause(message.GameState != Utility.EnumManager.GameState.Playing);
        }
    }
}