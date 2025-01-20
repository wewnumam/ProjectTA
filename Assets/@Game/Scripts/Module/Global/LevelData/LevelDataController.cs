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
        #region UTILITY

        private ISaveSystem<SavedLevelData> _savedLevelData = null;
        private IResourceLoader _resourceLoader = null;

        public void SetSaveSystem(ISaveSystem<SavedLevelData> savedLevelData)
        {
            _savedLevelData = savedLevelData;
        }

        public void SetResourceLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public void SetModel(LevelDataModel model)
        {
            _model = model;
        }

        #endregion

        public override IEnumerator Initialize()
        {
            if (_resourceLoader == null)
            {
                _resourceLoader = new ResourceLoader();
            }

            if (_savedLevelData == null)
            {
                _savedLevelData = new SaveSystem<SavedLevelData>(TagManager.FILENAME_SAVEDLEVELDATA);
            }
            _model.SetSavedLevelData(_savedLevelData.Load());

            SetCurrentLevel(_model.SavedLevelData.CurrentLevelName);
            SetCurrentCutscene(_model.SavedLevelData.CurrentCutsceneName);

            LoadLevelCollection();

            InitUnlockedLevel();

            yield return base.Initialize();
        }

        #region PRIVATE METHOD

        private void LoadLevelCollection()
        {
            SOLevelCollection levelCollection = _resourceLoader.Load<SOLevelCollection>(TagManager.SO_LEVELCOLLECTION);
            if (levelCollection != null)
            {
                _model.SetLevelCollection(levelCollection);
            }
            else
            {
                Debug.LogError($"LEVEL COLLECTION SCRIPTABLE NOT FOUND!");
            }
        }

        private void InitUnlockedLevel()
        {
            if (_model.LevelCollection == null ||
                _model.LevelCollection.LevelItems == null ||
                _model.LevelCollection.LevelItems.Count <= 0)
            {
                return;
            }

            foreach (var levelItem in _model.LevelCollection.LevelItems)
            {
                if (!levelItem.IsLockedLevel)
                {
                    OnUnlockLevel(new UnlockLevelMessage(levelItem));
                }
            }
        }

        private void SetCurrentLevel(string levelName)
        {
            SOLevelData levelData = _resourceLoader.Load<SOLevelData>(@"LevelData/" + levelName);
            if (levelData != null)
            {
                _model.SetCurrentLevelData(levelData);
                _model.SetCurrentEnvironmentPrefab(levelData.EnvironmentPrefab);
            }
            else
            { 
                Debug.LogError($"{levelName} SCRIPTABLE NOT FOUND!");
            }
        }

        private void SetCurrentCutscene(string cutsceneName)
        {
            SOCutsceneData cutsceneData = _resourceLoader.Load<SOCutsceneData>(@"CutsceneData/" + cutsceneName);
            if (cutsceneData != null)
            {
                _model.SetCurrentCutsceneData(cutsceneData);
            }
            else
            {
                Debug.LogError($"{cutsceneName} SCRIPTABLE NOT FOUND!");
            }
        }

        #endregion

        #region MESSAGE LISTENER

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

        #endregion
    }
}
