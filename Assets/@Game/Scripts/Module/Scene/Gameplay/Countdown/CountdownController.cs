using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using UnityEngine;

namespace ProjectTA.Module.Countdown
{
    public class CountdownController : ObjectController<CountdownController, CountdownModel, ICountdownModel, CountdownView>
    {
        public void InitModel(ILevelDataModel levelData)
        {
            if (levelData == null)
            {
                Debug.LogError("LEVELDATA IS NULL");
                return;
            }

            if (levelData.CurrentLevelData == null)
            {
                Debug.LogError("CURRENTLEVELDATA IS NULL");
                return;
            }

            _model.SetInitialCountdown(levelData.CurrentLevelData.Countdown);
            _model.SetCurrentCountdown(levelData.CurrentLevelData.Countdown);
        }


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

        public void OnGamePause(GamePauseMessage message)
        {
            _view.IsPause = true;
        }

        public void OnGameResume(GameResumeMessage message)
        {
            _view.IsPause = false;
        }

        public void OnRestart(CountdownRestartMessage message)
        {
            _model.SetCurrentCountdown(_model.InitialCountdown);
            Publish(new UpdateCountdownMessage(_model.InitialCountdown, _model.CurrentCountdown, true));
        }

        public void OnReset(CountdownResetMessage message)
        {
            _model.SetCurrentCountdown(0);
            Publish(new UpdateCountdownMessage(_model.InitialCountdown, _model.CurrentCountdown, false));
        }
    }
}