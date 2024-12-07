using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.EnemyManager
{
    public class EnemyManagerConnector : BaseConnector
    {
        private EnemyManagerController _enemyManager;

        protected override void Connect()
        {
            Subscribe<GamePauseMessage>(_enemyManager.OnGamePause);
            Subscribe<GameResumeMessage>(_enemyManager.OnGameResume);
            Subscribe<GameOverMessage>(_enemyManager.OnGameOver);
            Subscribe<GameWinMessage>(_enemyManager.OnGameWin);
            Subscribe<UpdateCountdownMessage>(_enemyManager.OnUpdateCountdown);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GamePauseMessage>(_enemyManager.OnGamePause);
            Unsubscribe<GameResumeMessage>(_enemyManager.OnGameResume);
            Unsubscribe<GameOverMessage>(_enemyManager.OnGameOver);
            Unsubscribe<GameWinMessage>(_enemyManager.OnGameWin);
            Unsubscribe<UpdateCountdownMessage>(_enemyManager.OnUpdateCountdown);
        }
    }
}