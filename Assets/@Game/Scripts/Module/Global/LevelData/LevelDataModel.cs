using Agate.MVC.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataModel : BaseModel, ILevelDataModel
    {
        public SOCutsceneData CurrentCutsceneData { get; private set; }
        public SOLevelData CurrentLevelData { get; private set; }
        public SOLevelCollection LevelCollection { get; private set; }

        public GameObject CurrentEnvironmentPrefab { get; private set; }

        public void SetCurrentCutsceneData(SOCutsceneData currentCutsceneData)
        {
            CurrentCutsceneData = currentCutsceneData;
            SetDataAsDirty();
        }

        public void SetCurrentLevelData(SOLevelData levelData)
        {
            CurrentLevelData = levelData;
            SetDataAsDirty();
        }

        public void SetLevelCollection(SOLevelCollection levelCollection)
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