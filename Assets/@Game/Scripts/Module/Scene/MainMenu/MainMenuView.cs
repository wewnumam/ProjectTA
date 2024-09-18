using Agate.MVC.Base;
using UnityEngine.UI;
using UnityEngine.Events;
using ProjectTA.Module.LevelSelection;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuView : BaseSceneView
    {
        public LevelSelectionView LevelSelectionView;

        public Button playButton;
        public Button quitButton;

        public void SetButtonCallback(UnityAction onPlay, UnityAction onQuit)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(onPlay);

            quitButton.onClick.RemoveAllListeners();
            quitButton.onClick.AddListener(onQuit);
        }
    }
}
