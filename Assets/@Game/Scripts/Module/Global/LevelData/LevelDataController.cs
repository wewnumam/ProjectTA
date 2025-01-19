using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataController : DataController<LevelDataController, LevelDataModel, ILevelDataModel>
    {
        private SaveSystem<SavedLevelData> _savedLevelData = null;

        public void SetModel(LevelDataModel model)
        {
            _model = model;
        }

        public override IEnumerator Initialize()
        {
            _savedLevelData = new(TagManager.FILENAME_SAVEDLEVELDATA);
            _model.SetUnlockedLevels(_savedLevelData.Load());
            SetCurrentLevel(_model.SavedLevelData.CurrentLevelName);
            SetCurrentCutscene(_model.SavedLevelData.CurrentCutsceneName);

            try
            {
                SOLevelCollection levelCollection = Resources.Load<SOLevelCollection>(TagManager.SO_LEVELCOLLECTION);
                _model.SetLevelCollection(levelCollection);
            }
            catch (Exception e)
            {
                Debug.LogError($"LEVEL COLLECTION SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            foreach (var levelItem in _model.LevelCollection.LevelItems)
            {
                if (!levelItem.IsLockedLevel)
                {
                    OnUnlockLevel(new UnlockLevelMessage(levelItem));
                }
            }

            yield return base.Initialize();
        }

        public void SetCurrentLevel(string levelName)
        {
            try
            {
                SOLevelData levelData = Resources.Load<SOLevelData>(@"LevelData/" + levelName);
                _model.SetCurrentLevelData(levelData);
                _model.SetCurrentEnvironmentPrefab(levelData.EnvironmentPrefab);
            }
            catch (Exception e)
            {
                Debug.LogError($"{levelName} SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }
        }

        public void SetCurrentCutscene(string cutsceneName)
        {
            try
            {
                SOCutsceneData cutsceneData = Resources.Load<SOCutsceneData>(@"CutsceneData/" + cutsceneName);
                _model.SetCurrentCutsceneData(cutsceneData);
            }
            catch (Exception e)
            {
                Debug.LogError($"{cutsceneName} SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }
        }

        public void OnChooseLevel(ChooseLevelMessage message)
        {
            SetCurrentLevel(message.LevelData.name);
            SetCurrentCutscene(message.LevelData.CutsceneData.name);
            _savedLevelData.Save(_model.SavedLevelData);
            Debug.Log($"CHOOSE LEVEL: {message.LevelData}");
        }

        public void OnUnlockLevel(UnlockLevelMessage message)
        {
            if (message.LevelData != null)
            {
                _model.AddUnlockedLevel(message.LevelData.name);
                _savedLevelData.Save(_model.SavedLevelData);
            }
            else
            {
                Debug.LogWarning($"LEVEL DATA MESSAGE IS NULL!");
            }
        }

        public void OnDeleteSaveData(DeleteSaveDataMessage message)
        {
            _savedLevelData.Delete();
        }
    }
}
