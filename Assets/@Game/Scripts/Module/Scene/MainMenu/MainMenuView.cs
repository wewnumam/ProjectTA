using Agate.MVC.Base;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuView : BaseSceneView
    {
        public Button playButton;

        public void SetButtonCallback(UnityAction onPlay)
        {
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(onPlay);
        }
    }
}
