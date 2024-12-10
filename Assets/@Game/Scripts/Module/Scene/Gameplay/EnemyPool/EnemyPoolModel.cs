using Agate.MVC.Base;
using ProjectTA.Module.Enemy;
using ProjectTA.Module.GameConstants;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.EnemyPool
{
    public class EnemyPoolModel : BaseModel
    {
        public bool IsPlaying { get; private set; }
        public bool IsGameCountdownBelowZero { get; private set; }
        public GameObject EnemyPrefab { get; private set; }
        public EnemyConstants EnemyConstants { get; private set; }

        private float _currentCountdown = 0;
        private List<GameObject> _enemyPool = new List<GameObject>();

        public void SetIsPlaying(bool isPlaying)
        {
            IsPlaying = isPlaying;
            SetDataAsDirty();
        }

        public void SetIsGameCountdownBelowZero(bool isGameCountdownBelowZero)
        {
            IsGameCountdownBelowZero = isGameCountdownBelowZero;
            SetDataAsDirty();
        }

        public void SetEnemyPrefab(GameObject enemyPrefab)
        {
            EnemyPrefab = enemyPrefab;
            SetDataAsDirty();
        }

        public void SetEnemyConstants(EnemyConstants enemyConstants)
        {
            EnemyConstants = enemyConstants;
            SetDataAsDirty();
        }

        public bool CanSpawn()
        {
            return IsPlaying && _currentCountdown <= 0;
        }

        public void ResetCountdown()
        {
            _currentCountdown = IsGameCountdownBelowZero ? EnemyConstants.EnemyMassiveSpawnInterval : EnemyConstants.EnemySpawnInterval;
        }

        public void DecreaseCountdown(float amount)
        {
            _currentCountdown -= amount;
        }

        public void AddEnemy(GameObject enemy)
        {
            _enemyPool.Add(enemy);
        }

        public GameObject GetPooledBullet()
        {
            for (int i = 0; i < EnemyConstants.EnemyPoolSize; i++)
            {
                if (!_enemyPool[i].activeInHierarchy)
                {
                    return _enemyPool[i];
                }
            }
            return null;
        }

        public void SetEnemyPause(bool pause)
        {
            foreach (GameObject enemy in _enemyPool)
            {
                if (enemy.TryGetComponent<EnemyView>(out var enemyView))
                {
                    enemyView.isPause = pause;
                }
            }
        }
    }
}
