using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.BulletPool
{
    public class BulletPoolView : BaseView
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _bulletSpawnPoint;
        private UnityAction<float> _onSpawnBullet;

        public GameObject BulletPrefab => _bulletPrefab;
        public Transform BulletSpawnPoint => _bulletSpawnPoint;

        public void SetCallback(UnityAction<float> onSpawnBullet)
        {
            _onSpawnBullet = onSpawnBullet;
        }

        private void Update()
        {
            _onSpawnBullet?.Invoke(Time.deltaTime);
        }
    }
}