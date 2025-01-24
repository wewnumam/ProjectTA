using Cinemachine;
using NUnit.Framework;
using ProjectTA.Module.CutscenePlayer;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class CutscenePlayerTests
    {
        private CutscenePlayerModel _model;
        private CutscenePlayerController _controller;

        [SetUp]
        public void Setup()
        {
            _model = new CutscenePlayerModel();
            _controller = new CutscenePlayerController();

            _controller.SetModel(_model);
        }

        [Test]
        public void InitStory_ShouldInitializesStory()
        {
            // Arrange
            var textAsset = Resources.Load<TextAsset>(@"Dialogues/Test/TestStory");

            // Act
            _model.InitStory(textAsset);

            // Assert
            Assert.IsNotNull(_model.Story);
        }

        [Test]
        public void SetNextLine_ShouldUpdatesCurrentLineText()
        {
            // Arrange
            var textAsset = Resources.Load<TextAsset>(@"Dialogues/Test/TestStory");
            _model.InitStory(textAsset);

            // Act
            _model.SetNextLine();
            var currentLine = TestUtility.GetPrivateField<string>(_model, "_currentLineText");

            // Assert
            Assert.AreEqual("Character: Hello World!\n", currentLine);
        }

        [Test]
        public void UpdateDialogueLine_WithCharacterName_ShouldParsesCorrectly()
        {
            // Arrange
            TestUtility.SetPrivateField(_model, "_currentLineText", "Alice: Hello World");

            // Act
            _model.UpdateDialogueLine();

            // Assert
            Assert.AreEqual("Alice", _model.CharacterName);
            Assert.AreEqual("Hello World", _model.Message);
        }

        [Test]
        public void UpdateDialogueLine_WithoutCharacterName_ShouldSetsEmptyName()
        {
            // Arrange
            TestUtility.SetPrivateField(_model, "_currentLineText", "Hello World");

            // Act
            _model.UpdateDialogueLine();

            // Assert
            Assert.AreEqual(string.Empty, _model.CharacterName);
            Assert.AreEqual("Hello World", _model.Message);
        }

        [Test]
        public void UpdateDialogueLine_ShouldSetsTextAnimationCompleteAction()
        {
            // Arrange
            TestUtility.SetPrivateField(_model, "_currentLineText", "Test message");
            _model.SetIsTextAnimationComplete(false);

            // Act
            _model.UpdateDialogueLine();
            _model.OnTextAnimationComplete?.Invoke();

            // Assert
            Assert.IsTrue(_model.IsTextAnimationComplete);
        }

        [UnityTest]
        public IEnumerator GoNextCamera_ShouldUpdatesCameraStatesCorrectly()
        {
            // Arrange
            var cameras = new List<CinemachineVirtualCamera>
        {
            new GameObject().AddComponent<CinemachineVirtualCamera>(),
            new GameObject().AddComponent<CinemachineVirtualCamera>(),
            new GameObject().AddComponent<CinemachineVirtualCamera>()
        };
            _model.SetCameras(cameras);

            // Act & Assert - First camera
            _model.GoNextCamera();
            Assert.IsTrue(cameras[0].enabled);
            Assert.IsFalse(cameras[1].enabled);
            Assert.IsFalse(cameras[2].enabled);

            // Act & Assert - Second camera
            _model.GoNextCamera();
            Assert.IsFalse(cameras[0].enabled);
            Assert.IsTrue(cameras[1].enabled);
            Assert.IsFalse(cameras[2].enabled);

            // Act & Assert - Third camera
            _model.GoNextCamera();
            Assert.IsFalse(cameras[0].enabled);
            Assert.IsFalse(cameras[1].enabled);
            Assert.IsTrue(cameras[2].enabled);

            // Act & Assert - Beyond camera count (disable all)
            _model.GoNextCamera();
            Assert.IsFalse(cameras[0].enabled);
            Assert.IsFalse(cameras[1].enabled);
            Assert.IsFalse(cameras[2].enabled);

            yield return null;
        }

        [Test]
        public void SetIsTextAnimationComplete_UpdatesProperty()
        {
            // Act & Assert
            _model.SetIsTextAnimationComplete(true);
            Assert.IsTrue(_model.IsTextAnimationComplete);

            _model.SetIsTextAnimationComplete(false);
            Assert.IsFalse(_model.IsTextAnimationComplete);
        }

        [Test]
        public void GetLog_ContainsAllRelevantInformation()
        {
            // Arrange
            var cameras = new List<CinemachineVirtualCamera>
        {
            new GameObject().AddComponent<CinemachineVirtualCamera>(),
            new GameObject().AddComponent<CinemachineVirtualCamera>()
        };
            _model.SetCameras(cameras);
            TestUtility.SetPrivateField(_model, "_currentCameraIndex", 1);
            TestUtility.SetPrivateField(_model, "_currentLineText", "Test: Line");
            _model.UpdateDialogueLine();
            _model.SetIsTextAnimationComplete(true);
            _model.OnTextAnimationComplete = () => { };

            // Act
            var log = _model.GetLog();

            // Assert
            StringAssert.Contains("_cameras Count: 2", log);
            StringAssert.Contains("_currentCameraIndex: 1", log);
            StringAssert.Contains("CharacterName: Test", log);
            StringAssert.Contains("Message: Line", log);
            StringAssert.Contains("IsTextAnimationComplete: True", log);
            StringAssert.Contains("OnTextAnimationComplete: Set", log);
        }

        [UnityTest]
        public IEnumerator Init_ShouldInitStoryAndSetCameras()
        {
            // Arrange
            var cutsceneData = Resources.Load<SOCutsceneData>(@"CutsceneData/CutsceneData_Intro");

            // Act
            _controller.Init(cutsceneData);

            // Assert
            Assert.IsNotNull(_model.Story);
            Assert.AreEqual(cutsceneData.Environment.Cameras.Count, TestUtility.GetPrivateField<List<CinemachineVirtualCamera>>(_model, "_cameras").Count);

            yield return null;
        }
    }
}