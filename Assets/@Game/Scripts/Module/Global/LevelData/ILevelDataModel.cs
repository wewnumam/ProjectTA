using Agate.MVC.Base;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public interface ILevelDataModel : IBaseModel
    {
        SOCutsceneData CurrentCutsceneData { get; }
        SOLevelData CurrentLevelData { get; }
        SOLevelCollection LevelCollection { get; }
        SavedLevelData SavedLevelData { get; }

        GameObject CurrentEnvironmentPrefab { get; }

        List<SOLevelData> GetUnlockedLevels();
    }
}