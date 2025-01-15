using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public interface ILevelDataModel : IBaseModel
    {
        SOCutsceneData CurrentCutsceneData { get; }
        SOLevelData CurrentLevelData { get; }
        SOLevelCollection LevelCollection { get; }

        GameObject CurrentEnvironmentPrefab { get; }
    }
}