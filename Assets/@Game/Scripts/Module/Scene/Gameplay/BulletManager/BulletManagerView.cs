using Agate.MVC.Base;
using Agate.MVC.Core;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerView : BaseView
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawnPoint;
        public Vector3 rotationOffset;

        [ReadOnly] public float shootingRate;
        [ReadOnly] public bool isShoot;

        public void StartShoot()
        {
            isShoot = true;
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot()
        {
            while (isShoot)
            {
                GameObject.Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                yield return new WaitForSeconds(shootingRate);
            }
        }
    }
}