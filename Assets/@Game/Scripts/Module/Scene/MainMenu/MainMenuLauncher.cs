using ProjectTA.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectTA.Message;
using UnityEngine;
using ProjectTA.Utility;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.Tutorial;
using ProjectTA.Module.CollectibleList;
using System;
using ProjectTA.Module.QuestData;
using ProjectTA.Module.QuestList;
using ProjectTA.Module.Settings;
using ProjectTA.Module.GameSettings;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuLauncher : SceneLauncher<MainMenuLauncher, MainMenuView>
    {
        public override string SceneName {get {return TagManager.SCENE_MAINMENU;}}

        private readonly SaveSystemController _saveSystem = new();
        private readonly CollectibleDataController _collectibleData = new();
        private readonly CollectibleListController _collectibleList = new();
        private readonly QuestDataController _questData = new();
        private readonly GameSettingsController _gameSettings = new();

        private readonly TutorialController _tutorial = new();
        private readonly QuestListController _questList = new();
        private readonly SettingsController _settings = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new TutorialController(),
                new CollectibleListController(),
                new QuestListController(),
                new SettingsController(),
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

            SetInitialUnlockedCollectibles();
            SetInitialQuestData();

            _tutorial.SetView(_view.TutorialView);

            _collectibleList.SetCollectibleCollection(_collectibleData.Model.CollectibleCollection);
            _collectibleList.SetUnlockedCollectibles(_collectibleData.Model.UnlockedCollectibleItems);
            _collectibleList.SetView(_view.CollectibleListView);

            _questList.SetQuestCollection(_questData.Model.QuestCollection);
            _questList.SetQuestData(_questData.Model.CurrentQuestData);
            _questList.SetView(_view.QuestListView);

            _settings.SetInitialVolume(_gameSettings.Model.AudioVolume);
            _settings.SetInitialVibrate(_gameSettings.Model.IsVibrateOn);
            _settings.SetView(_view.SettingsView);

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
            if (_saveSystem.Model.SaveData.UnlockedCollectibles.Count > 0 && _collectibleData.Model.UnlockedCollectibleItems.Count <= 0)
            {
                foreach (var collectibleName in _saveSystem.Model.SaveData.UnlockedCollectibles)
                {
                    _collectibleData.AddUnlockedCollectible(collectibleName);
                }
            }
        }

        private void SetInitialQuestData()
        {
            _questData.SetCurrentQuestData(_saveSystem.Model.SaveData.CurrentQuestData);
            _questData.SetCollectibleCollectionAndUnlockedCollectible(_collectibleData.Model.CollectibleCollection, _saveSystem.Model.SaveData.UnlockedCollectibles);
            _questData.SetCurrentLevelPlayedAmount(_saveSystem.Model.SaveData.LevelPlayed.Count);
        }
    }
}
