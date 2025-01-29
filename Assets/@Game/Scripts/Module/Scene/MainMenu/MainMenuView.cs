using Agate.MVC.Base;
using ProjectTA.Module.BugReport;
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
        [field:SerializeField]
        public TutorialView TutorialView { get; private set; }
        [field: SerializeField]
        public CollectibleListView CollectibleListView { get; private set; }
        [field: SerializeField]
        public QuestListView QuestListView { get; private set; }
        [field: SerializeField]
        public SettingsView SettingsView { get; private set; }
        [field: SerializeField]
        public BugReportView BugReportView { get; private set; }

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
