using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerConnector : BaseConnector
    {
        private BulletManagerController _bulletManager;

        protected override void Connect()
        {
            Subscribe<PlayerCharacterShootStartMessage>(_bulletManager.OnShootStart);
            Subscribe<PlayerCharacterShootEndMessage>(_bulletManager.OnShootEnd);
        }

        protected override void Disconnect()
        {
            Unsubscribe<PlayerCharacterShootStartMessage>(_bulletManager.OnShootStart);
            Unsubscribe<PlayerCharacterShootEndMessage>(_bulletManager.OnShootEnd);
        }
    }
}