using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.BulletPool
{
    public class BulletPoolConnector : BaseConnector
    {
        private readonly BulletPoolController _bulletPool = new();

        protected override void Connect()
        {
            Subscribe<RotatePlayerCharacterMessage>(_bulletPool.OnAim);
            Subscribe<DespawnBulletMessage>(_bulletPool.OnDespawnBullet);
            Subscribe<GameStateMessage>(_bulletPool.OnGameState);
        }

        protected override void Disconnect()
        {
            Unsubscribe<RotatePlayerCharacterMessage>(_bulletPool.OnAim);
            Unsubscribe<DespawnBulletMessage>(_bulletPool.OnDespawnBullet);
            Unsubscribe<GameStateMessage>(_bulletPool.OnGameState);
        }
    }
}