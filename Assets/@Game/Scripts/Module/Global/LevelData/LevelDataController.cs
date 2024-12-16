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
        private readonly SaveSystemController _saveSystemController = new();

        public override IEnumerator Initialize()
        {
            SOLevelCollection levelCollection = Resources.Load<SOLevelCollection>(@"LevelCollection");
            _model.SetLevelCollection(levelCollection);

            yield return base.Initialize();
        }

        public IEnumerator SetCurrentLevel(string levelName)
        {
            SOLevelData levelData = Resources.Load<SOLevelData>(@"LevelData/" + levelName);
            _model.SetCurrentLevelData(levelData);
            _model.SetCurrentEnvironmentPrefab(levelData.EnvironmentPrefab);

            yield return null;
        }

        public IEnumerator SetCurrentCutscene(string cutsceneName)
        {
            SOCutsceneData cutsceneData = Resources.Load<SOCutsceneData>(@"CutsceneData/" + cutsceneName);
            _model.SetCurrentCutsceneData(cutsceneData);

            yield return null;
        }

        internal void OnChooseLevel(ChooseLevelMessage message)
        {
            Debug.Log($"CHOOSE LEVEL: {message.LevelData}");
            GameMain.Instance.RunCoroutine(SetCurrentLevel(message.LevelData.name));
            _saveSystemController.SetCurrentLevelName(message.LevelData.name);
            _saveSystemController.SetCurrentCutsceneName(message.LevelData.CutsceneData.name);
        }
    }
}
