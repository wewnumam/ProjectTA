using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelSelection
{
    public interface ILevelSelectionPlayerModel : IBaseModel
    {
        SOLevelCollection LevelCollection { get; }
        SOLevelData CurrentLevelData { get; }
        List<string> UnlockedLevels { get; }
    }
}