using ProjectTA.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectTA.Message;
using UnityEngine;
using ProjectTA.Utility;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuLauncher : SceneLauncher<MainMenuLauncher, MainMenuView>
    {
        public override string SceneName {get {return TagManager.SCENE_MAINMENU;}}

        LevelDataController _levelData;
        SaveSystemController _saveSystem;
        CollectibleDataController _collectibleData;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
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

            _view.SetCallbacks(OnPlay, OnQuit, OnAchievement);

            foreach (var collectibleName in _saveSystem.Model.SaveData.UnlockedCollectibles)
            {
                _collectibleData.AddUnlockedCollectible(collectibleName);
            }

            yield return null;
        }

        private void OnPlay()
        {
            if (_saveSystem.Model.SaveData.CurrentCutsceneName == TagManager.DEFAULT_CUTSCENENAME)
            {
                SceneLoader.Instance.LoadScene(TagManager.SCENE_CUTSCENE);
                foreach (var levelItem in _levelData.Model.LevelCollection.levelItems)
                {
                    if (!levelItem.isLockedLevel)
                    {
                        Publish(new UnlockLevelMessage(levelItem));
                    }
                }
            }
            else
            {
                SceneLoader.Instance.LoadScene(TagManager.SCENE_LEVELSELECTION);
            }
        }

        private void OnQuit()
        {
            Application.Quit();
        }

        private void OnAchievement()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_ACHIEVEMENT);
        }
    }
}
