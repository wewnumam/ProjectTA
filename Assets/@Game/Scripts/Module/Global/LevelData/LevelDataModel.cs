using Agate.MVC.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataModel : BaseModel, ILevelDataModel
    {
        public SO_CutsceneData CurrentCutsceneData { get; private set; }
        public SO_LevelData CurrentLevelData { get; private set; }
        public SO_LevelCollection LevelCollection { get; private set; }

        public GameObject CurrentEnvironmentPrefab { get; private set; }

        public void SetCurrentCutsceneData(SO_CutsceneData currentCutsceneData)
        {
            CurrentCutsceneData = currentCutsceneData;
            SetDataAsDirty();
        }

        public void SetCurrentLevelData(SO_LevelData levelData)
        {
            CurrentLevelData = levelData;
            SetDataAsDirty();
        }

        public void SetLevelCollection(SO_LevelCollection levelCollection)
        {
            LevelCollection = levelCollection;
            SetDataAsDirty();
        }

        public void SetCurrentEnvironmentPrefab(GameObject currentEnvironmentPrefab)
        {
            CurrentEnvironmentPrefab = currentEnvironmentPrefab;
            SetDataAsDirty();
        }
    }
}