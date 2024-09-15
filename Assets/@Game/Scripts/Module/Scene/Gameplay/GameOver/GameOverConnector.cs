using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.GameOver
{
    public class GameOverConnector : BaseConnector
    {
        private GameOverController _gameOver;

        protected override void Connect()
        {
            Subscribe<GameOverMessage>(_gameOver.OnGameOver);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GameOverMessage>(_gameOver.OnGameOver);
        }
    }
}