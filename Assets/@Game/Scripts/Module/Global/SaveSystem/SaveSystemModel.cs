using Agate.MVC.Base;

namespace ProjectTA.Module.SaveSystem
{
    public class SaveSystemModel : BaseModel, ISaveSystemModel
    {
        public SaveData SaveData { get; private set; }

        public void SetSaveData(SaveData saveData)
        {
            SaveData = saveData;
            SetDataAsDirty();
        }

        public void SetCurrentCutsceneName(string cutsceneName)
        {
            SaveData.CurrentCutsceneName = cutsceneName;
            SetDataAsDirty();
        }

        public void SetCurrentLevelName(string levelName)
        {
            SaveData.CurrentLevelName = levelName;
            SetDataAsDirty();
        }

        public void AddUnlockedLevel(string levelName)
        {
            SaveData.UnlockedLevels.Add(levelName);
            SetDataAsDirty();
        }

        public bool IsLevelUnlocked(string levelName)
        {
            SetDataAsDirty();
            return SaveData.UnlockedLevels.Contains(levelName);
        }
    }
}