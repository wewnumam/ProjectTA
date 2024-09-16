using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerConnector : BaseConnector
    {
        private BulletManagerController _bulletManager;

        protected override void Connect()
        {
            Subscribe<PlayerCharacterShootMessage>(_bulletManager.OnShoot);
        }

        protected override void Disconnect()
        {
            Unsubscribe<PlayerCharacterShootMessage>(_bulletManager.OnShoot);
        }
    }
}