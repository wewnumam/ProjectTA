using Agate.MVC.Base;
using UnityEngine.UI;
using UnityEngine.Events;
using ProjectTA.Module.Tutorial;
using ProjectTA.Module.CollectibleList;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuView : BaseSceneView
    {
        public TutorialView TutorialView;
        public CollectibleListView CollectibleListView;

        private UnityAction _onPlay, _onQuit, _onQuiz;

        public void Play()
        {
            _onPlay.Invoke();
        }

        public void Quit()
        {
            _onQuit?.Invoke();
        }

        public void Quiz()
        {
            _onQuiz?.Invoke();
        }

        public void SetCallbacks(UnityAction onPlay, UnityAction onQuit, UnityAction onQuiz)
        {
            _onPlay = onPlay;
            _onQuit = onQuit;
            _onQuiz = onQuiz;
        }
    }
}
