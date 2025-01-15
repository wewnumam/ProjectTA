using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.GoogleFormUploader;
using ProjectTA.Module.QuizPlayer;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTA.Scene.Quiz
{
    public class QuizLauncher : SceneLauncher<QuizLauncher, QuizView>
    {
        public override string SceneName { get { return TagManager.SCENE_QUIZ; } }

        private readonly GameConstantsController _gameConstants = new();

        private readonly QuizPlayerController _quizPlayer = new();
        private readonly GoogleFormUploaderController _googleFormUploader = new();

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new QuizPlayerController(),
                new GoogleFormUploaderController(),
            };
        }

        protected override IConnector[] GetSceneConnectors()
        {
            return new IConnector[] {
                new GoogleFormUploaderConnector(),
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

            _quizPlayer.SetView(_view.QuizPlayerView);

            _googleFormUploader.SetQuizFormConstants(_gameConstants.Model.GameConstants.QuizFormConstants);
            _googleFormUploader.SetView(_view.GoogleFormUploaderView);

            yield return null;
        }
    }
}
