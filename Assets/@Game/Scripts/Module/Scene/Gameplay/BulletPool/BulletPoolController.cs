using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.Bullet;
using ProjectTA.Module.GameConstants;
using System;
using UnityEngine;

namespace ProjectTA.Module.BulletPool
{
    public class BulletPoolController : ObjectController<BulletPoolController, BulletPoolModel, BulletPoolView>
    {
        public void SetShootingConstants(ShootingConstants shootingConstants) => _model.SetShootingConstants(shootingConstants);

        public override void SetView(BulletPoolView view)
        {
            base.SetView(view);
            SetPoolObject();
            view.SetCallback(OnSpawnBullet);
        }

        private void OnSpawnBullet(float deltaTime)
        {
            if (_model.CanShoot())
            {
                SpawnBullet();
                _model.ResetCountdown();
            }
            _model.DecreaseCountdown(deltaTime);
        }

        private void SetPoolObject()
        {
            for (int i = 0; i < _model.ShootingConstants.BulletPoolSize; i++)
            {
                _model.AddBullet(CreateBulletObject());
            }
        }

        private GameObject CreateBulletObject()
        {
            GameObject bullet = GameObject.Instantiate(_view.bulletPrefab, _view.bulletSpawnPoint.position, Quaternion.identity);
            bullet.SetActive(false);

            BulletView bulletView = bullet.GetComponent<BulletView>();
            if (bulletView == null)
            {
                return null;
            }

            BulletController bulletController = new BulletController();
            InjectDependencies(bulletController);
            bulletController.Init(bulletView, _model.ShootingConstants.BulletDestroyDelay);

            return bullet;
        }


        private void SpawnBullet()
        {
            GameObject bullet = _model.GetPooledBullet();
            if (bullet != null)
            {
                bullet.transform.SetParent(null);
                bullet.transform.position = _view.bulletSpawnPoint.position;
                bullet.transform.rotation = _view.bulletSpawnPoint.rotation;
                bullet.SetActive(true);
            }
        }

        internal void OnAim(RotatePlayerCharacterMessage message)
        {
            _model.SetIsShooting(message.Aim.sqrMagnitude > _model.ShootingConstants.JoystickShootingRange);
        }

        internal void OnDespawnBullet(DespawnBulletMessage message)
        {
            message.Bullet.SetActive(false);
        }

        internal void OnGameState(GameStateMessage message)
        {
            _model.SetIsPlaying(message.GameState == Utility.EnumManager.GameState.Playing);
        }
    }
}