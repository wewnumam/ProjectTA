using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelSelection
{
    public interface ILevelSelectionPlayerModel : IBaseModel
    {
        SOLevelData CurrentLevelData { get; }
        int CurrentLevelDataIndex { get; }

        bool IsCurrentLevelUnlocked();
        string GetLog();
    }
}