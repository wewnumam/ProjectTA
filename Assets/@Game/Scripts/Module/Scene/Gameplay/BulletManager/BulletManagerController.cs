using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameState;
using UnityEngine;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerController : ObjectController<BulletManagerController, BulletManagerView>
    {
        GameStateController gameState;

        internal void OnShoot(PlayerCharacterShootMessage message)
        {
            if (gameState.IsStatePlaying())
                GameObject.Instantiate(_view.bulletPrefab, _view.bulletSpawnPoint.position, _view.bulletSpawnPoint.rotation);
        }
    }
}