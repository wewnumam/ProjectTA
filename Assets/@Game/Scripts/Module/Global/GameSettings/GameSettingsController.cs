using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System.Collections;
using System.IO;
using UnityEngine;

namespace ProjectTA.Module.SaveSystem
{
    public class GameSettingsController : DataController<GameSettingsController, GameSettingsModel, IGameSettingsModel>
    {
        private SaveSystem<SavedSettingsData> _savedSettingsData = null;

        public void SetModel(GameSettingsModel model)
        {
            _model = model;
        }

        public override IEnumerator Initialize()
        {
            _savedSettingsData = new SaveSystem<SavedSettingsData>(TagManager.FILENAME_SAVEDGAMESETTINGS);
            _model.SetSaveData(_savedSettingsData.Load());

            yield return base.Initialize();
        }

        #region MESSAGE LISTENER

        public void DeleteSaveData(DeleteSaveDataMessage message)
        {
            _savedSettingsData.Delete();
        }

        public void ToggleGameInduction(ToggleGameInductionMessage message)
        {
            _model.SetIsGameIndctionActive(message.IsGameInductionActive);
            _savedSettingsData.Save(_model.SavedSettingsData);
        }

        public void ToggleSfx(ToggleSfxMessage message)
        {
            _model.SetIsSfxOn(message.Sfx);
            _savedSettingsData.Save(_model.SavedSettingsData);
        }

        public void ToggleBgm(ToggleBgmMessage message)
        {
            _model.SetIsBgmOn(message.Bgm);
            _savedSettingsData.Save(_model.SavedSettingsData);
        }

        public void ToggleVibration(ToggleVibrationMessage message)
        {
            _model.SetIsVibrationOn(message.Vibration);
            _savedSettingsData.Save(_model.SavedSettingsData);
        }

        #endregion
    }
}
