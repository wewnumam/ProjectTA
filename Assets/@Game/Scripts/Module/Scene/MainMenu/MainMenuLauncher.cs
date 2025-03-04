using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.BugReport;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.CollectibleList;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.QuestData;
using ProjectTA.Module.QuestList;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Module.Settings;
using ProjectTA.Module.Tutorial;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuLauncher : SceneLauncher<MainMenuLauncher, MainMenuView>
    {
        public override string SceneName { get { return TagManager.SCENE_MAINMENU; } }

        private readonly GameSettingsController _gameSettings = new();
        private readonly LevelDataController _levelData = new();
        private readonly CollectibleDataController _collectibleData = new();
        private readonly CollectibleListController _collectibleList = new();
        private readonly QuestDataController _questData = new();

        private readonly TutorialController _tutorial = new();
        private readonly QuestListController _questList = new();
        private readonly SettingsController _settings = new();
        private readonly BugReportController _bugReport = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new TutorialController(),
                new CollectibleListController(),
                new QuestListController(),
                new SettingsController(),
                new BugReportController(),
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

            PlayerPrefs.SetString(TagManager.KEY_VERSION, Application.version);

            _view.SetCallbacks(OnPlay, OnQuit, OnQuiz);

            _tutorial.SetView(_view.TutorialView);

            _collectibleList.InitModel(_collectibleData.Model);
            _collectibleList.SetView(_view.CollectibleListView);

            _questList.InitModel(_questData.Model);
            _questList.SetView(_view.QuestListView);

            _settings.InitModel(_gameSettings.Model);
            _settings.SetView(_view.SettingsView);

            _bugReport.SetView(_view.BugReportView);

            _view.OnInitDone?.Invoke();

            yield return null;
        }

        private void OnPlay()
        {
            if (_levelData.Model.SavedLevelData.CurrentCutsceneName == TagManager.DEFAULT_CUTSCENENAME)
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
            Publish(new AppQuitMessage());
            Application.Quit();
        }

        private void OnQuiz()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_QUIZ);
        }
    }
}
