using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelSelection;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.LevelSelection
{
    public class LevelSelectionLauncher : SceneLauncher<LevelSelectionLauncher, LevelSelectionView>
    {
        public override string SceneName { get { return TagManager.SCENE_LEVELSELECTION; } }

        private readonly SaveSystemController _saveSystem = new();
        private readonly LevelDataController _levelData = new();

        private readonly LevelSelectionPlayerController _levelSelection = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new LevelSelectionPlayerController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
            };
        }

        protected override IEnumerator LaunchScene()
        {
            yield return null;
        }

        protected override IEnumerator InitSceneObject()
        {
            Time.timeScale = 1;

            Publish(new GameStateMessage(EnumManager.GameState.PreGame));

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName));

            yield return StartCoroutine(_levelData.SetCurrentLevel(_saveSystem.Model.SaveData.CurrentLevelName));

            if (_saveSystem.Model.SaveData.UnlockedLevels.Count <= 0)
            {
                SetInitialUnlockedLevels();
            }

            _levelSelection.SetLevelCollection(_levelData.Model.LevelCollection);
            List<SOLevelData> unlockedLevels = new();
            foreach (var levelData in _levelData.Model.LevelCollection.LevelItems)
            {
                if (_saveSystem.Model.SaveData.UnlockedLevels.Contains(levelData.name))
                {
                    unlockedLevels.Add(levelData);
                }
            }
            _levelSelection.SetUnlockedLevels(unlockedLevels);
            _levelSelection.SetView(_view.levelSelectionView);

            yield return null;
        }

        private void SetInitialUnlockedLevels()
        {
            foreach (var levelItem in _levelData.Model.LevelCollection.LevelItems)
            {
                if (!levelItem.IsLockedLevel)
                {
                    Publish(new UnlockLevelMessage(levelItem));
                }
            }
        }
    }
}
