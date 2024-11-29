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
        }

        protected override void Disconnect()
        {
            Unsubscribe<GamePauseMessage>(_enemyManager.OnGamePause);
            Unsubscribe<GameResumeMessage>(_enemyManager.OnGameResume);
        }
    }
}