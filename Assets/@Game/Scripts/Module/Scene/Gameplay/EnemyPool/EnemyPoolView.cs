using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.EnemyPool
{
    public class EnemyPoolView : BaseView
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Camera _mainCamera;

        public Transform Player => _player;
        public Camera MainCamera => _mainCamera;

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