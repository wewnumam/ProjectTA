using ProjectTA.Utility;
using System.Collections.Generic;

namespace ProjectTA.Module.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        public string CurrentCutsceneName = TagManager.DEFAULT_CUTSCENENAME;
        public string CurrentLevelName = TagManager.DEFAULT_LEVELNAME;
        public List<string> UnlockedLevels = new List<string>();
        public List<string> UnlockedCollectibles = new List<string>();
    }
}