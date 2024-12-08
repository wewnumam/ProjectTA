using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.GameWin
{
    public class GameWinConnector : BaseConnector
    {
        private GameWinController _gameWin;

        protected override void Connect()
        {
            Subscribe<UpdateKillCountMessage>(_gameWin.OnUpdateKillCount);
            Subscribe<UpdatePuzzleCountMessage>(_gameWin.OnUpdatePuzzleCount);
            Subscribe<UpdateHiddenObjectCountMessage>(_gameWin.OnUpdateHiddenObjectCount);
            Subscribe<UpdateCountdownMessage>(_gameWin.OnUpdateCountdown);
            Subscribe<GameWinMessage>(_gameWin.OnGameWin);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdateKillCountMessage>(_gameWin.OnUpdateKillCount);
            Unsubscribe<UpdatePuzzleCountMessage>(_gameWin.OnUpdatePuzzleCount);
            Unsubscribe<UpdateHiddenObjectCountMessage>(_gameWin.OnUpdateHiddenObjectCount);
            Unsubscribe<UpdateCountdownMessage>(_gameWin.OnUpdateCountdown);
            Unsubscribe<GameWinMessage>(_gameWin.OnGameWin);
        }
    }
}