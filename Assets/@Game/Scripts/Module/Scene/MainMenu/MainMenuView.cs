using Agate.MVC.Base;
using UnityEngine.Events;
using ProjectTA.Module.Tutorial;
using ProjectTA.Module.CollectibleList;
using UnityEngine;
using ProjectTA.Module.QuestList;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuView : BaseSceneView
    {
        [SerializeField] private TutorialView _tutorialView;
        [SerializeField] private CollectibleListView _collectibleListView;
        [SerializeField] private QuestListView _questListView;

        public TutorialView TutorialView => _tutorialView;
        public CollectibleListView CollectibleListView => _collectibleListView;
        public QuestListView QuestListView => _questListView;

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
