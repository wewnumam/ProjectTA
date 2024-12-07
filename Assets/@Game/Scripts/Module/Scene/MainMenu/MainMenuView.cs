using Agate.MVC.Base;
using UnityEngine.UI;
using UnityEngine.Events;
using ProjectTA.Module.Tutorial;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuView : BaseSceneView
    {
        public TutorialView TutorialView;

        private UnityAction _onPlay, _onQuit, _onAchievement;

        public void Play()
        {
            _onPlay.Invoke();
        }

        public void Quit()
        {
            _onQuit?.Invoke();
        }

        public void Achievement()
        {
            _onAchievement?.Invoke();
        }

        public void SetCallbacks(UnityAction onPlay, UnityAction onQuit, UnityAction onAchievement)
        {
            _onPlay = onPlay;
            _onQuit = onQuit;
            _onAchievement = onAchievement;
        }
    }
}
