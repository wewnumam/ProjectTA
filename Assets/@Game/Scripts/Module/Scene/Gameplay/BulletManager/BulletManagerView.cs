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

        [ReadOnly] public bool isJoystickActive;
        [ReadOnly] public float shootingRate;
        [ReadOnly] public bool isShooting;

        [Range(0f, 1f)]
        public float joystickShootRange;

        public Coroutine shootingCoroutine;

        public void StartShooting()
        {
            if (!isShooting)
            {
                isShooting = true;
                shootingCoroutine = StartCoroutine(Shoot());
            }
        }

        private IEnumerator Shoot()
        {
            while (isShooting)
            {
                GameObject.Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                yield return new WaitForSeconds(shootingRate);
            }
        }
    }
}