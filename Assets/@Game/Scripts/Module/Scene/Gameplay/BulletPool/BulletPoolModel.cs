using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.BulletPool
{
    public class BulletPoolModel : BaseModel, IBulletPoolModel
    {
        public bool IsPlaying { get; private set; }
        public bool IsShooting { get; private set; }
        public ShootingConstants ShootingConstants { get; private set; }

        private float _currentCountdown = 0;

        private List<GameObject> _bulletPool = new List<GameObject>();

        public void SetIsPlaying(bool isPlaying)
        {
            IsPlaying = isPlaying;
            SetDataAsDirty();
        }

        public void SetIsShooting(bool isShooting)
        {
            IsShooting = isShooting;
            SetDataAsDirty();
        }

        public void SetShootingConstants(ShootingConstants shootingConstants)
        {
            ShootingConstants = shootingConstants;
            SetDataAsDirty();
        }

        public bool CanShoot()
        {
            return IsShooting && IsPlaying && _currentCountdown <= 0;
        }

        public void ResetCountdown()
        {
            _currentCountdown = ShootingConstants.BulletShootingRate;
        }

        public void DecreaseCountdown(float amount)
        {
            _currentCountdown -= amount;
        }

        public void AddBullet(GameObject bullet)
        {
            _bulletPool.Add(bullet);
        }

        public GameObject GetPooledBullet()
        {
            for (int i = 0; i < ShootingConstants.BulletPoolSize; i++)
            {
                if (!_bulletPool[i].activeInHierarchy)
                {
                    return _bulletPool[i];
                }
            }
            return null;
        }
    }
}
