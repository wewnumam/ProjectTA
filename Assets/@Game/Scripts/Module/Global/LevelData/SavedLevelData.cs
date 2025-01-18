using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [Serializable]
    public class SavedLevelData
    {
        [field: SerializeField]
        public string CurrentCutsceneName { get; set; } = TagManager.DEFAULT_CUTSCENENAME;
        [field: SerializeField]
        public string CurrentLevelName { get; set; } = TagManager.DEFAULT_LEVELNAME;
        [field: SerializeField]
        public List<string> UnlockedLevels { get; set; } = new();
    }
}