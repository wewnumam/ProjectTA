using NUnit.Framework;
using ProjectTA.Boot;
using ProjectTA.Scene.Cutscene;
using ProjectTA.Scene.Gameplay;
using ProjectTA.Scene.LevelSelection;
using ProjectTA.Scene.MainMenu;
using ProjectTA.Scene.Quiz;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    public class SceneLoaderTests
    {
        private string expectedLogMessage;
        private bool logMessageReceived = false;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            if (GameMain.Instance != null)
            {
                GameObject.Destroy(GameMain.Instance.gameObject);
            }

            if (SplashScreen.Instance != null)
            {
                GameObject.Destroy(SplashScreen.Instance.gameObject);
            }

            if (SceneLoader.Instance != null)
            {
                GameObject.Destroy(SceneLoader.Instance.gameObject);
            }

            // Loop through all loaded scenes
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                UnityEngine.SceneManagement.Scene scene = SceneManager.GetSceneAt(i);
                if (scene.isLoaded)
                {
                    // Unload the scene asynchronously
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
        }

        [UnityTest]
        public IEnumerator SceneLoader_LoadScene_ShouldLoadSceneCorrectly()
        {
            SceneManager.LoadScene(TagManager.SCENE_MAINMENU);

            string expectedLog = "Finish Load " + TagManager.SCENE_MAINMENU;
            LogAssert.Expect(LogType.Log, expectedLog);
            yield return WaitForSpecificLog(expectedLog);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_MAINMENU);
            yield return new WaitUntil(() => GameObject.FindAnyObjectByType<MainMenuLauncher>() != null);
            Assert.IsNotNull(GameObject.FindAnyObjectByType<MainMenuLauncher>());

            expectedLog = "Finish Load " + TagManager.SCENE_GAMEPLAY;
            LogAssert.Expect(LogType.Log, expectedLog);
            SceneLoader.Instance.LoadScene(TagManager.SCENE_GAMEPLAY);
            yield return WaitForSpecificLog(expectedLog);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_GAMEPLAY);
            yield return new WaitUntil(() => GameObject.FindAnyObjectByType<GameplayLauncher>() != null);
            Assert.IsNotNull(GameObject.FindAnyObjectByType<GameplayLauncher>());

            expectedLog = "Finish Load " + TagManager.SCENE_LEVELSELECTION;
            LogAssert.Expect(LogType.Log, expectedLog);
            SceneLoader.Instance.LoadScene(TagManager.SCENE_LEVELSELECTION);
            yield return WaitForSpecificLog(expectedLog);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_LEVELSELECTION);
            yield return new WaitUntil(() => GameObject.FindAnyObjectByType<LevelSelectionLauncher>() != null);
            Assert.IsNotNull(GameObject.FindAnyObjectByType<LevelSelectionLauncher>());

            expectedLog = "Finish Load " + TagManager.SCENE_CUTSCENE;
            LogAssert.Expect(LogType.Log, expectedLog);
            SceneLoader.Instance.LoadScene(TagManager.SCENE_CUTSCENE);
            yield return WaitForSpecificLog(expectedLog);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_CUTSCENE);
            yield return new WaitUntil(() => GameObject.FindAnyObjectByType<CutsceneLauncher>() != null);
            Assert.IsNotNull(GameObject.FindAnyObjectByType<CutsceneLauncher>());

            expectedLog = "Finish Load " + TagManager.SCENE_QUIZ;
            LogAssert.Expect(LogType.Log, expectedLog);
            SceneLoader.Instance.LoadScene(TagManager.SCENE_QUIZ);
            yield return WaitForSpecificLog(expectedLog);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_QUIZ);
            yield return new WaitUntil(() => GameObject.FindAnyObjectByType<QuizLauncher>() != null);
            Assert.IsNotNull(GameObject.FindAnyObjectByType<QuizLauncher>());
        }

        [UnityTest]
        public IEnumerator SceneManager_LoadScene_MainMenu()
        {
            SceneManager.LoadScene(TagManager.SCENE_MAINMENU);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_MAINMENU);

            var launcher = GameObject.FindAnyObjectByType<MainMenuLauncher>();
            Assert.IsNotNull(launcher);
            var view = GameObject.FindAnyObjectByType<MainMenuView>();
            Assert.IsNotNull(view);
        }

        [UnityTest]
        public IEnumerator SceneManager_LoadScene_Gameplay()
        {
            SceneManager.LoadScene(TagManager.SCENE_GAMEPLAY);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_GAMEPLAY);

            var launcher = GameObject.FindAnyObjectByType<GameplayLauncher>();
            Assert.IsNotNull(launcher);
            var view = GameObject.FindAnyObjectByType<GameplayView>();
            Assert.IsNotNull(view);
        }

        [UnityTest]
        public IEnumerator SceneManager_LoadScene_LevelSelection()
        {
            SceneManager.LoadScene(TagManager.SCENE_LEVELSELECTION);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_LEVELSELECTION);

            var launcher = GameObject.FindAnyObjectByType<LevelSelectionLauncher>();
            Assert.IsNotNull(launcher);
            var view = GameObject.FindAnyObjectByType<LevelSelectionView>();
            Assert.IsNotNull(view);
        }

        [UnityTest]
        public IEnumerator SceneManager_LoadScene_Cutscene()
        {
            SceneManager.LoadScene(TagManager.SCENE_CUTSCENE);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_CUTSCENE);

            var launcher = GameObject.FindAnyObjectByType<CutsceneLauncher>();
            Assert.IsNotNull(launcher);
            var view = GameObject.FindAnyObjectByType<CutsceneView>();
            Assert.IsNotNull(view);
        }

        [UnityTest]
        public IEnumerator SceneManager_LoadScene_Quiz()
        {
            SceneManager.LoadScene(TagManager.SCENE_QUIZ);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_QUIZ);

            var launcher = GameObject.FindAnyObjectByType<QuizLauncher>();
            Assert.IsNotNull(launcher);
            var view = GameObject.FindAnyObjectByType<QuizView>();
            Assert.IsNotNull(view);
        }

        #region Helper Method

        // Coroutine that waits for a specific log message
        public IEnumerator WaitForSpecificLog(string expectedMessage)
        {
            expectedLogMessage = expectedMessage;
            logMessageReceived = false;

            // Subscribe to the log event
            Application.logMessageReceived += HandleLog;

            // Wait until the expected log message is received
            while (!logMessageReceived)
            {
                yield return null; // Wait for the next frame
            }

            // Unsubscribe from the log event once we're done
            Application.logMessageReceived -= HandleLog;

            Debug.Log("Expected log message received: " + expectedMessage);
        }

        // Log handler method
        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (logString.Contains(expectedLogMessage))
            {
                logMessageReceived = true;
            }
        }

        #endregion
    }
}
