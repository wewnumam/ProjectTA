using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.EnemyManager
{
    public class EnemyManagerView : BaseView
    {
        public Transform player;
        public GameObject enemyPrefab; // Reference to the enemy prefab
        public float spawnDistance = 10f; // Distance from the camera where enemies should spawn
        public float spawnInterval = 5f; // Interval in seconds between spawns
        public Camera mainCamera;
        public UnityEvent onEnemySpawn;
        public List<EnemyView> enemies;

        [ReadOnly] public int enemyCount;

        void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
        }

        private void SpawnEnemy()
        {
            onEnemySpawn?.Invoke();
        }

        public void SetCallback(UnityAction onEnemySpawn)
        {
            this.onEnemySpawn.RemoveAllListeners();
            this.onEnemySpawn.AddListener(onEnemySpawn);
        }
    }
}