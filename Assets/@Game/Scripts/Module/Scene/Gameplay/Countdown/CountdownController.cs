using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.Countdown
{
    public class CountdownController : ObjectController<CountdownController , CountdownModel, ICountdownModel, CountdownView>
    {
        public void SetInitialCountdown(float countdown) => _model.SetInitialCountdown(countdown);
        public void SetCurrentCountdown(float countdown) => _model.SetCurrentCountdown(countdown);

        public override void SetView(CountdownView view)
        {
            base.SetView(view);
            view.SetCallback(OnUpdate);
        }

        private void OnUpdate(float deltaTime)
        {
            _model.SubtractCurrentCountdown(deltaTime);
            Publish(new UpdateCountdownMessage(_model.InitialCountdown, _model.CurrentCountdown, false));
        }

        internal void OnGamePause(GamePauseMessage message)
        {
            _view.IsPause = true;
        }

        internal void OnGameResume(GameResumeMessage message)
        {
            _view.IsPause = false;
        }

        internal void OnRestart(CountdownRestartMessage message)
        {
            _model.SetCurrentCountdown(_model.InitialCountdown);
            Publish(new UpdateCountdownMessage(_model.InitialCountdown, _model.CurrentCountdown, true));
        }

        internal void OnReset(CountdownResetMessage message)
        {
            _model.SetCurrentCountdown(0);
            Publish(new UpdateCountdownMessage(_model.InitialCountdown, _model.CurrentCountdown, false));
        }
    }
}