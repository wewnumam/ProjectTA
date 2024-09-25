using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.SaveSystem;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataController : DataController<LevelDataController, LevelDataModel, ILevelDataModel>
    {
        private SaveSystemController _saveSystemController;

        public override IEnumerator Initialize()
        {
            SO_LevelCollection levelCollection = Resources.Load<SO_LevelCollection>(@"LevelCollection");
            _model.SetLevelCollection(levelCollection);

            yield return base.Initialize();
        }

        public IEnumerator SetCurrentLevel(string levelName)
        {
            SO_LevelData levelData = Resources.Load<SO_LevelData>(@"LevelData/" + levelName);
            _model.SetCurrentLevelData(levelData);
            _model.SetCurrentEnvironmentPrefab(levelData.environmentPrefab);

            yield return null;
        }

        internal void OnChooseLevel(ChooseLevelMessage message)
        {
            Debug.Log($"CHOOSE LEVEL: {message.LevelData}");
            GameMain.Instance.RunCoroutine(SetCurrentLevel(message.LevelData.name));
            _saveSystemController.SetCurrentLevelName(message.LevelData.name);
        }
    }
}
