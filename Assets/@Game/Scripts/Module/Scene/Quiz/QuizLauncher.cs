using ProjectTA.Boot;
using Agate.MVC.Base;
using Agate.MVC.Core;
using System.Collections;
using UnityEngine.SceneManagement;
using ProjectTA.Message;
using UnityEngine;
using ProjectTA.Utility;
using ProjectTA.Module.QuizPlayer;

namespace ProjectTA.Scene.Quiz
{
    public class QuizLauncher : SceneLauncher<QuizLauncher, QuizView>
    {
        public override string SceneName {get {return TagManager.SCENE_QUIZ;}}

        private QuizPlayerController _quizPlayer;

        protected override IController[] GetSceneDependencies()
        {
            return new IController[] {
                new QuizPlayerController(),
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

            _quizPlayer.SetView(_view.QuizPlayerView);

            yield return null;
        }
    }
}
