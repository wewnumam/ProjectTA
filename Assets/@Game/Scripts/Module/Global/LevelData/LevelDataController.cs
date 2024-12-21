using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataController : DataController<LevelDataController, LevelDataModel, ILevelDataModel>
    {
        private string _levelCollectionFileName = "LevelCollection";

        public void SetModel(LevelDataModel model)
        {
            _model = model;
        }

        public void SetLevelCollectionFileName(string fileName)
        {
            _levelCollectionFileName = fileName;
        }

        public override IEnumerator Initialize()
        {
            SOLevelCollection levelCollection = Resources.Load<SOLevelCollection>(_levelCollectionFileName);
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

        public void OnChooseLevel(ChooseLevelMessage message)
        {
            GameMain.Instance.RunCoroutine(SetCurrentLevel(message.LevelData.name));
            Debug.Log($"CHOOSE LEVEL: {message.LevelData}");
        }
    }
}
