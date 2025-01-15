using Agate.MVC.Base;
using ProjectTA.Module.CollectibleList;
using ProjectTA.Module.QuestList;
using ProjectTA.Module.Settings;
using ProjectTA.Module.Tutorial;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Scene.MainMenu
{
    public class MainMenuView : BaseSceneView
    {
        [SerializeField] private TutorialView _tutorialView;
        [SerializeField] private CollectibleListView _collectibleListView;
        [SerializeField] private QuestListView _questListView;
        [SerializeField] private SettingsView _settingsView;

        public TutorialView TutorialView => _tutorialView;
        public CollectibleListView CollectibleListView => _collectibleListView;
        public QuestListView QuestListView => _questListView;
        public SettingsView SettingsView => _settingsView;

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
