using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.GameOver
{
    public class GameOverConnector : BaseConnector
    {
        private readonly GameOverController _gameOver = new();

        protected override void Connect()
        {
            Subscribe<UpdateKillCountMessage>(_gameOver.OnUpdateKillCount);
            Subscribe<UpdatePuzzleCountMessage>(_gameOver.OnUpdatePuzzleCount);
            Subscribe<UpdateHiddenObjectCountMessage>(_gameOver.OnUpdateHiddenObjectCount);
            Subscribe<GameOverMessage>(_gameOver.OnGameOver);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdateKillCountMessage>(_gameOver.OnUpdateKillCount);
            Unsubscribe<UpdatePuzzleCountMessage>(_gameOver.OnUpdatePuzzleCount);
            Unsubscribe<UpdateHiddenObjectCountMessage>(_gameOver.OnUpdateHiddenObjectCount);
            Unsubscribe<GameOverMessage>(_gameOver.OnGameOver);
        }
    }
}