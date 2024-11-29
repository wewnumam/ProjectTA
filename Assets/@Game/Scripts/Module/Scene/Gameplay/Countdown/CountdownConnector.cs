using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Countdown
{
    public class CountdownConnector : BaseConnector
    {
        private CountdownController _countdown;

        protected override void Connect()
        {
            Subscribe<GamePauseMessage>(_countdown.OnGamePause);
            Subscribe<GameResumeMessage>(_countdown.OnGameResume);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GamePauseMessage>(_countdown.OnGamePause);
            Unsubscribe<GameResumeMessage>(_countdown.OnGameResume);
        }
    }
}