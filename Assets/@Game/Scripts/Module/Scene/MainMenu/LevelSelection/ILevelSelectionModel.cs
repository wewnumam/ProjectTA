using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelSelection
{
    public interface ILevelSelectionModel : IBaseModel
    {
        SO_LevelCollection LevelCollection { get; }
        SO_LevelData CurrentLevelData { get; }
    }
}