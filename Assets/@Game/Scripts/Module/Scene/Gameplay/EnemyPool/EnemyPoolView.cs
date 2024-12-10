using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.EnemyPool
{
    public class EnemyPoolView : BaseView
    {
        public Transform player;
        public Camera mainCamera;

        private UnityAction<float> _onSpawnEnemy;

        public void SetCallback(UnityAction<float> onSpawnEnemy)
        {
            _onSpawnEnemy = onSpawnEnemy;
        }

        private void Update()
        {
            _onSpawnEnemy?.Invoke(Time.deltaTime);
        }
    }
}