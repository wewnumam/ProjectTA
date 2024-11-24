using Agate.MVC.Base;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public interface ILevelDataModel : IBaseModel
    {
        SO_CutsceneData CurrentCutsceneData { get; }
        SO_LevelData CurrentLevelData { get; }
        SO_LevelCollection LevelCollection { get; }

        GameObject CurrentEnvironmentPrefab { get; }
    }
}