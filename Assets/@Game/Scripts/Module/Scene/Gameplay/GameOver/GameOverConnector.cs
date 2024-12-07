using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.GameOver
{
    public class GameOverConnector : BaseConnector
    {
        private GameOverController _gameOver;

        protected override void Connect()
        {
            Subscribe<UpdateKillCountMessage>(_gameOver.OnUpdateKillCount);
            Subscribe<UpdatePuzzleCountMessage>(_gameOver.OnUpdatePuzzleCount);
            Subscribe<GameOverMessage>(_gameOver.OnGameOver);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdateKillCountMessage>(_gameOver.OnUpdateKillCount);
            Unsubscribe<UpdatePuzzleCountMessage>(_gameOver.OnUpdatePuzzleCount);
            Unsubscribe<GameOverMessage>(_gameOver.OnGameOver);
        }
    }
}