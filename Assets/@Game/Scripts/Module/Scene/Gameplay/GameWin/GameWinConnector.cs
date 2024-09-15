using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.GameWin
{
    public class GameWinConnector : BaseConnector
    {
        private GameWinController _gameWin;

        protected override void Connect()
        {
            Subscribe<GameWinMessage>(_gameWin.OnGameWin);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GameWinMessage>(_gameWin.OnGameWin);
        }
    }
}