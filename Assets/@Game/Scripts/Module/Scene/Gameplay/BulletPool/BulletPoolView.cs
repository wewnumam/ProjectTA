using Agate.MVC.Base;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.BulletPool
{
    public class BulletPoolView : BaseView
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawnPoint;

        private UnityAction<float> _onSpawnBullet;

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