using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterConnector : BaseConnector
    {
        private PlayerCharacterController _playerCharacter;

        protected override void Connect()
        {
            Subscribe<GameOverMessage>(_playerCharacter.OnGameOver);
            Subscribe<GameWinMessage>(_playerCharacter.OnGameWin);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GameOverMessage>(_playerCharacter.OnGameOver);
            Unsubscribe<GameWinMessage>(_playerCharacter.OnGameWin);
        }
    }
}