using NUnit.Framework;
using ProjectTA.Module.CutscenePlayer;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class CutscenePlayerModelTest
    {
        private CutscenePlayerModel _cutscenePlayerModel;

        [SetUp]
        public void SetUp()
        {
            _cutscenePlayerModel = new CutscenePlayerModel();
        }

        [Test]
        public void InitStory_ShouldInitializeStory_WhenGivenTextAsset()
        {
            // Arrange
            TextAsset textAsset = Resources.Load<TextAsset>(@"Dialogues/Test/TestStory");
            // Act
            _cutscenePlayerModel.InitStory(textAsset);

            // Assert
            Assert.IsNotNull(_cutscenePlayerModel.Story);
            Assert.AreEqual("Character: Hello World!", _cutscenePlayerModel.Story.Continue()?.Trim());
        }

        [Test]
        public void SetNextLine_ShouldUpdateCharacterNameAndMessage_WhenCalled()
        {
            // Arrange
            TextAsset textAsset = Resources.Load<TextAsset>(@"Dialogues/Test/TestStory");
            _cutscenePlayerModel.InitStory(textAsset);

            // Act
            _cutscenePlayerModel.SetNextLine();
            _cutscenePlayerModel.UpdateDialogueLine();

            // Assert
            Assert.AreEqual("Character", _cutscenePlayerModel.CharacterName);
            Assert.AreEqual("Hello World!", _cutscenePlayerModel.Message);
        }

        [Test]
        public void GoNextCamera_ShouldSwitchToNextCamera_WhenCalled()
        {
            // Arrange
            var camera1 = new GameObject().AddComponent<Cinemachine.CinemachineVirtualCamera>();
            var camera2 = new GameObject().AddComponent<Cinemachine.CinemachineVirtualCamera>();
            _cutscenePlayerModel.SetCameras(new List<Cinemachine.CinemachineVirtualCamera> { camera1, camera2 });

            // Act
            _cutscenePlayerModel.GoNextCamera();

            // Assert
            Assert.IsTrue(camera1.enabled);
            Assert.IsFalse(camera2.enabled);
        }

        [Test]
        public void UpdateDialogueLine_ShouldUpdateDialogue_WhenCalled()
        {
            // Arrange
            TextAsset textAsset = Resources.Load<TextAsset>(@"Dialogues/Test/TestStory");
            _cutscenePlayerModel.InitStory(textAsset);
            _cutscenePlayerModel.SetNextLine();
            _cutscenePlayerModel.UpdateDialogueLine();

            // Act
            _cutscenePlayerModel.SetNextLine();
            _cutscenePlayerModel.UpdateDialogueLine();

            // Assert
            Assert.AreEqual("Character", _cutscenePlayerModel.CharacterName);
            Assert.AreEqual("Goodbye!", _cutscenePlayerModel.Message);
        }

        [Test]
        public void SetIsTextAnimationComplete_ShouldUpdateAnimationState_WhenCalled()
        {
            // Arrange
            // Act
            _cutscenePlayerModel.SetIsTextAnimationComplete(true);

            // Assert
            Assert.IsTrue(_cutscenePlayerModel.IsTextAnimationComplete);
        }

        [Test]
        public void GetLog_ShouldReturnLogInfo_WhenCalled()
        {
            // Arrange
            TextAsset textAsset = Resources.Load<TextAsset>(@"Dialogues/Test/TestStory");
            _cutscenePlayerModel.InitStory(textAsset);
            _cutscenePlayerModel.SetNextLine();
            _cutscenePlayerModel.UpdateDialogueLine();

            // Act
            string log = _cutscenePlayerModel.GetLog();

            // Assert
            Assert.IsNotNull(log);
            Assert.IsTrue(log.Contains("CutscenePlayerModel Log:"));
        }
    }
}