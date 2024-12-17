using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Countdown
{
    public class CountdownConnector : BaseConnector
    {
        private readonly CountdownController _countdown = new();

        protected override void Connect()
        {
            Subscribe<GamePauseMessage>(_countdown.OnGamePause);
            Subscribe<GameResumeMessage>(_countdown.OnGameResume);
            Subscribe<CountdownRestartMessage>(_countdown.OnRestart);
            Subscribe<CountdownResetMessage>(_countdown.OnReset);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GamePauseMessage>(_countdown.OnGamePause);
            Unsubscribe<GameResumeMessage>(_countdown.OnGameResume);
            Unsubscribe<CountdownRestartMessage>(_countdown.OnRestart);
            Unsubscribe<CountdownResetMessage>(_countdown.OnReset);
        }
    }
}