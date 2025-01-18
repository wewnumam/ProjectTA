using Agate.MVC.Base;
using ProjectTA.Module.QuizPlayer;
using System.Collections.Generic;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemModel : BaseModel, ISaveSystemModel
    {
        public SaveData SaveData { get; private set; } = new();

        public void SetSaveData(SaveData saveData)
        {
            SaveData = saveData;
            SetDataAsDirty();
        }

        public void SetIsGameIndctionActive(bool isGameIndctionActive)
        {
            SaveData.IsGameInductionActive = isGameIndctionActive;
            SetDataAsDirty();
        }

        public void SetIsSfxOn(bool isSfxOn)
        {
            SaveData.IsSfxOn = isSfxOn;
            SetDataAsDirty();
        }

        public void SetIsBgmOn(bool isBgmOn)
        {
            SaveData.IsBgmOn = isBgmOn;
            SetDataAsDirty();
        }

        public void SetIsVibrationOn(bool isVibrationOn)
        {
            SaveData.IsVibrationOn = isVibrationOn;
            SetDataAsDirty();
        }
    }
}