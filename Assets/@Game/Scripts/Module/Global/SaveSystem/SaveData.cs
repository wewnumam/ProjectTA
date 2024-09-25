using ProjectTA.Utility;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SaveData
{
    public string CurrentLevelName = TagManager.DEFAULT_LEVELNAME;
    public List<string> UnlockedLevels = new List<string>();
}
