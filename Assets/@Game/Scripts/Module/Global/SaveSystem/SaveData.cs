using ProjectTA.Module.QuizPlayer;
using ProjectTA.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        [field: SerializeField]
        public string CurrentCutsceneName { get; set; } = TagManager.DEFAULT_CUTSCENENAME;
        [field: SerializeField]
        public string CurrentLevelName { get; set; } = TagManager.DEFAULT_LEVELNAME;
        [field: SerializeField]
        public List<string> UnlockedLevels { get; set; } = new List<string>();
        [field: SerializeField]
        public List<string> LevelPlayed { get; set; } = new List<string>();
        [field: SerializeField]
        public List<string> UnlockedCollectibles { get; set; } = new List<string>();
        [field: SerializeField]
        public List<ChoicesRecord> ChoicesRecords { get; set; } = new() { new ChoicesRecord(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)};
        [field: SerializeField]
        public QuestData.QuestData CurrentQuestData { get; set; } = new();
        [field: SerializeField]
        public bool IsGameInductionActive { get; set; } = true;
    }
}