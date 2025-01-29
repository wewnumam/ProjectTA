using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using UnityEngine;

namespace ProjectTA.Module.Health
{
    public class HealthController : DataController<HealthController, HealthModel>
    {
        public void SetModel(HealthModel model)
        {
            _model = model;
        }

        public void InitModel(ILevelDataModel levelData)
        {
            if (!ValidateLevelData(levelData))
                return;

            _model.SetInitialHealth(levelData.CurrentLevelData.InitialHealth);
            _model.SetCurrentHealth(levelData.CurrentLevelData.InitialHealth);
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, true));
        }

        #region PRIVATE METHOD

        private bool ValidateLevelData(ILevelDataModel levelData)
        {
            if (levelData == null)
                return LogError("LEVELDATA IS NULL");

            if (levelData.CurrentLevelData == null)
                return LogError("CURRENTLEVELDATA IS NULL");

            return true;
        }

        private bool LogError(string message)
        {
            Debug.LogError(message);
            return false;
        }

        #endregion

        #region MESSAGE LISTENER

        public void OnAdjustHealthCount(AdjustHealthCountMessage message)
        {
            _model.AdjustCurrentHealthCount(message.Amount);
            Publish(new UpdateHealthMessage(_model.InitialHealth, _model.CurrentHealth, message.Amount > 0));
            if (_model.IsCurrentHealthEqualsOrLessThanZero())
                Publish(new GameOverMessage());
        }

        #endregion
    }
}