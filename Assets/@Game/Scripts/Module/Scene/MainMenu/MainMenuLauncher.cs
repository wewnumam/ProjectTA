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
using ProjectTA.Module.Tutorial;
using ProjectTA.Module.CollectibleList;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuLauncher : SceneLauncher<MainMenuLauncher, MainMenuView>
    {
        public override string SceneName {get {return TagManager.SCENE_MAINMENU;}}

        private readonly LevelDataController _levelData = new();
        private readonly SaveSystemController _saveSystem = new();
        private readonly CollectibleDataController _collectibleData = new();
        private readonly CollectibleListController _collectibleList = new();

        private readonly TutorialController _tutorial = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new TutorialController(),
                new CollectibleListController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new CollectibleListConnector(),
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

            _view.SetCallbacks(OnPlay, OnQuit, OnQuiz);

            if (_saveSystem.Model.SaveData.UnlockedCollectibles.Count > 0  && _collectibleData.Model.UnlockedCollectibleItems.Count <= 0)
            {
                SetInitialUnlockedCollectibles();
            }

            _tutorial.SetView(_view.TutorialView);

            _collectibleList.SetCollectibleCollection(_collectibleData.Model.CollectibleCollection);
            _collectibleList.SetUnlockedCollectibles(_collectibleData.Model.UnlockedCollectibleItems);
            _collectibleList.SetView(_view.CollectibleListView);

            

            yield return null;
        }

        private void OnPlay()
        {
            if (_saveSystem.Model.SaveData.CurrentCutsceneName == TagManager.DEFAULT_CUTSCENENAME)
            {
                SceneLoader.Instance.LoadScene(TagManager.SCENE_CUTSCENE);
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

        private void OnQuiz()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_QUIZ);
        }

        private void SetInitialUnlockedCollectibles()
        {
            foreach (var collectibleName in _saveSystem.Model.SaveData.UnlockedCollectibles)
            {
                _collectibleData.AddUnlockedCollectible(collectibleName);
            }
        }
    }
}
