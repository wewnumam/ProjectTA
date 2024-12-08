using ProjectTA.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectTA.Message;
using UnityEngine;
using ProjectTA.Utility;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.CollectibleList;
using ProjectTA.Module.SaveSystem;

namespace ProjectTA.Scene.Achievement
{
    public class AchievementLauncher : SceneLauncher<AchievementLauncher, AchievementView>
    {
        public override string SceneName {get {return TagManager.SCENE_ACHIEVEMENT;}}

        SaveSystemController _saveSystem;

        CollectibleDataController _collectibleData;
        CollectibleListController _collectibleList;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new CollectibleListController(),
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

            foreach (var collectibleName in _saveSystem.Model.SaveData.UnlockedCollectibles)
            {
                _collectibleData.AddUnlockedCollectible(collectibleName);
            }

            _view.SetCallback(OnMainMenu);

            _collectibleList.SetCollectibleCollection(_collectibleData.Model.CollectibleCollection);
            _collectibleList.SetUnlockedCollectibles(_collectibleData.Model.UnlockedCollectibleItems);
            _collectibleList.SetView(_view.CollectibleListView);

            yield return null;
        }

        private void OnMainMenu()
        {
            SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);
        }
    }
}
