using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerController : ObjectController<BulletManagerController, BulletManagerView>
    {
        internal void OnShoot(PlayerCharacterShootMessage message)
        {
            GameObject.Instantiate(_view.bulletPrefab, _view.bulletSpawnPoint.position, _view.bulletSpawnPoint.rotation);
        }
    }
}