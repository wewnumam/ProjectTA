using Agate.MVC.Base;
using ProjectTA.Module.CollectibleList;
using UnityEngine.Events;

namespace ProjectTA.Scene.Achievement
{
    public class AchievementView : BaseSceneView
    {
        public CollectibleListView CollectibleListView;

        private UnityAction _onMainMenu;

        public void MainMenu()
        {
            _onMainMenu?.Invoke();
        }

        public void SetCallback(UnityAction onMainMenu)
        {
            _onMainMenu = onMainMenu;
        }
    }
}
