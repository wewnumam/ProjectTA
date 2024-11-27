using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerConnector : BaseConnector
    {
        private BulletManagerController _bulletManager;

        protected override void Connect()
        {
            Subscribe<ActivateJoystickMessage>(_bulletManager.OnActivateJoystick);
            Subscribe<PlayerCharacterShootStartMessage>(_bulletManager.OnShootStart);
            Subscribe<PlayerCharacterShootEndMessage>(_bulletManager.OnShootEnd);
            Subscribe<RotatePlayerCharacterMessage>(_bulletManager.OnAim);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ActivateJoystickMessage>(_bulletManager.OnActivateJoystick);
            Unsubscribe<PlayerCharacterShootStartMessage>(_bulletManager.OnShootStart);
            Unsubscribe<PlayerCharacterShootEndMessage>(_bulletManager.OnShootEnd);
            Unsubscribe<RotatePlayerCharacterMessage>(_bulletManager.OnAim);
        }
    }
}