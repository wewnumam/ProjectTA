using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.SaveSystem;
using UnityEngine;

namespace ProjectTA.Module.GameInduction
{
    public class GameInductionController : ObjectController<GameInductionController, GameInductionModel, GameInductionView>
    {
        public void InitModel(IGameSettingsModel gameSettings)
        {
            if (gameSettings == null)
            {
                Debug.LogError("GAMESETTINGS IS NULL");
                return;
            }

            if (gameSettings.SavedSettingsData == null)
            {
                Debug.LogError("SAVEDSETTINGSDATA IS NULL");
                return;
            }

            _model.SetIsGameInductionActive(gameSettings.SavedSettingsData.IsGameInductionActive);
        }

        public override void SetView(GameInductionView view)
        {
            base.SetView(view);
            view.SetCallback(OnCloseGameInduction);
            if (_model.IsGameInductionActive)
            {
                view.StartGameInduction();
                Publish(new ToggleGameInductionMessage(false));
            }
        }

        private void OnCloseGameInduction()
        {
            if (_model.IsGameInductionActive)
            {
                Time.timeScale = 1;
            }
        }
    }
}