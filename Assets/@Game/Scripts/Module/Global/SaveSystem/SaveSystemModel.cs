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

        public void SetCurrentCharacterName(string characterName)
        {
            SaveData.CurrentCharacterName = characterName;
            SetDataAsDirty();
        }

        public void SetCurrentLevelName(string levelName)
        {
            SaveData.CurrentLevelName = levelName;
            SetDataAsDirty();
        }

        public void AddHeart(int amount)
        {
            SaveData.CurrentHeartCount += amount;
            SetDataAsDirty();
        }

        public void SubtractHeart(int amount)
        {
            SaveData.CurrentHeartCount -= amount;
            SetDataAsDirty();
        }

        internal bool IsNewStar(int star)
        {
            return star > SaveData.GetStarRecordsByLevelName(SaveData.CurrentLevelName).StarCount;
        }

        public void SetStarRecord(int starAmount)
        {
            SaveData.GetStarRecordsByLevelName(SaveData.CurrentLevelName).StarCount = starAmount;
            SetDataAsDirty();
        }

        internal bool IsNewHighScore(int score)
        {
            return score > SaveData.GetStarRecordsByLevelName(SaveData.CurrentLevelName).HighScore;
        }

        internal void SetHighscore(int score)
        {
            SaveData.GetStarRecordsByLevelName(SaveData.CurrentLevelName).HighScore = score;
            SetDataAsDirty();
        }

        public void AddStarRecord(string LevelName)
        {
            StarRecords starRecords = new StarRecords(LevelName, 0, 0);
            SaveData.UnlockedLevels.Add(starRecords);
            SetDataAsDirty();
        }

        public void FullStar()
        {
            SaveData.UnlockedLevels.ForEach(level => level.StarCount = 5);
            SetDataAsDirty();
        }

        public void AddUnlockedCharacter(string characterName)
        {
            SaveData.UnlockedCharacters.Add(characterName);
            SetDataAsDirty();
        }
    }
}