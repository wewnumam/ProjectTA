using Agate.MVC.Base;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public interface ILevelDataModel : IBaseModel
    {
        SO_LevelData CurrentLevelData { get; }
        SO_LevelCollection LevelCollection { get; }

        Sprite CurrentArtwork { get; }
        List<GameObject> CurrentStonePrefabs { get; }
        GameObject CurrentEnvironmentPrefab { get; }
        AudioClip CurrentMusicClip { get; }
        Material CurrentSkybox { get; }
    }
}