using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterConnector : BaseConnector
    {
        private readonly PlayerCharacterController _playerCharacter = new();

        protected override void Connect()
        {
            Subscribe<MovePlayerCharacterMessage>(_playerCharacter.OnMove);
            Subscribe<RotatePlayerCharacterMessage>(_playerCharacter.OnRotate);
            Subscribe<ActivateJoystickMessage>(_playerCharacter.OnActivateJoystick);
            Subscribe<GameOverMessage>(_playerCharacter.OnGameOver);
            Subscribe<ToggleVibrationMessage>(_playerCharacter.OnVibrate);
        }

        protected override void Disconnect()
        {
            Unsubscribe<MovePlayerCharacterMessage>(_playerCharacter.OnMove);
            Unsubscribe<RotatePlayerCharacterMessage>(_playerCharacter.OnRotate);
            Unsubscribe<ActivateJoystickMessage>(_playerCharacter.OnActivateJoystick);
            Unsubscribe<GameOverMessage>(_playerCharacter.OnGameOver);
            Unsubscribe<ToggleVibrationMessage>(_playerCharacter.OnVibrate);
        }
    }
}