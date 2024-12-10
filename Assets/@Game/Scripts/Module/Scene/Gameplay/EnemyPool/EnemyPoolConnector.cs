    using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.EnemyPool
{
    public class EnemyPoolConnector : BaseConnector
    {
        private EnemyPoolController _enemyPool;

        protected override void Connect()
        {
            Subscribe<UpdateCountdownMessage>(_enemyPool.OnUpdateCountdown);
            Subscribe<GameStateMessage>(_enemyPool.OnGameState);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdateCountdownMessage>(_enemyPool.OnUpdateCountdown);
            Unsubscribe<GameStateMessage>(_enemyPool.OnGameState);
        }
    }
}