using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class LevelDataTests
    {
        private Mock<IResourceLoader> _mockResourceLoader;
        private Mock<ISaveSystem<SavedLevelData>> _mockSaveSystem;
        private LevelDataModel _model;
        private LevelDataController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockResourceLoader = new Mock<IResourceLoader>();
            _mockSaveSystem = new Mock<ISaveSystem<SavedLevelData>>();
            _model = new LevelDataModel();
            _controller = new LevelDataController();

            // Inject dependencies
            _controller.SetModel(_model);
            _controller.SetSaveSystem(_mockSaveSystem.Object);
            _controller.SetResourceLoader(_mockResourceLoader.Object);
        }

        [Test]
        public void SetCurrentCutsceneData_ShouldSetCutsceneData()
        {
            // Arrange
            var cutsceneData = ScriptableObject.CreateInstance<SOCutsceneData>();
            cutsceneData.name = "Cutscene1";

            // Act
            _model.SetCurrentCutsceneData(cutsceneData);

            // Assert
            Assert.AreEqual(cutsceneData, _model.CurrentCutsceneData);
            Assert.AreEqual("Cutscene1", _model.SavedLevelData.CurrentCutsceneName);
        }

        [Test]
        public void SetCurrentLevelData_ShouldSetLevelData()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            levelData.name = "Level1";

            // Act
            _model.SetCurrentLevelData(levelData);

            // Assert
            Assert.AreEqual(levelData, _model.CurrentLevelData);
            Assert.AreEqual("Level1", _model.SavedLevelData.CurrentLevelName);
        }

        [Test]
        public void SetLevelCollection_ShouldSetCollection()
        {
            // Arrange
            var levelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();

            // Act
            _model.SetLevelCollection(levelCollection);

            // Assert
            Assert.AreEqual(levelCollection, _model.LevelCollection);
        }

        [Test]
        public void SetCurrentEnvironmentPrefab_ShouldSetEnvironmentPrefab()
        {
            // Arrange
            var environmentPrefab = new GameObject();

            // Act
            _model.SetCurrentEnvironmentPrefab(environmentPrefab);

            // Assert
            Assert.AreEqual(environmentPrefab, _model.CurrentEnvironmentPrefab);
        }

        [Test]
        public void SetSavedLevelData_ShouldSetSavedData()
        {
            // Arrange
            var savedData = new SavedLevelData();
            savedData.CurrentLevelName = "Level2";

            // Act
            _model.SetSavedLevelData(savedData);

            // Assert
            Assert.AreEqual(savedData, _model.SavedLevelData);
        }

        [Test]
        public void AddUnlockedLevel_ShouldAddLevelToUnlockedList()
        {
            // Arrange
            var levelName = "Level3";

            // Act
            _model.AddUnlockedLevel(levelName);

            // Assert
            Assert.Contains(levelName, _model.SavedLevelData.UnlockedLevels);
        }

        [Test]
        public void AddUnlockedLevel_ShouldNotAddDuplicateLevels()
        {
            // Arrange
            var levelName = "Level3";
            _model.AddUnlockedLevel(levelName);

            // Act
            _model.AddUnlockedLevel(levelName);

            // Assert
            Assert.AreEqual(1, _model.SavedLevelData.UnlockedLevels.Count);
        }

        [Test]
        public void GetUnlockedLevels_ShouldReturnUnlockedLevelData()
        {
            // Arrange
            var level1 = ScriptableObject.CreateInstance<SOLevelData>();
            level1.name = "Level1";

            var level2 = ScriptableObject.CreateInstance<SOLevelData>();
            level2.name = "Level2";

            var levelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();
            levelCollection.LevelItems = new List<SOLevelData> { level1, level2 };

            _model.SetLevelCollection(levelCollection);
            _model.AddUnlockedLevel("Level1");

            // Act
            var unlockedLevels = _model.GetUnlockedLevels();

            // Assert
            Assert.Contains(level1, unlockedLevels);
            Assert.IsFalse(unlockedLevels.Contains(level2));
        }

        [Test]
        public void Initialize_ShouldLoadSavedDataAndSetCurrentLevelAndCutscene()
        {
            // Arrange
            var savedLevelData = new SavedLevelData();
            savedLevelData.CurrentLevelName = "Level1";
            savedLevelData.CurrentCutsceneName = "Cutscene1";

            _mockSaveSystem
                .Setup(x => x.Load())
                .Returns(savedLevelData);
            _mockResourceLoader
                .Setup(x => x.Load<SOLevelCollection>(@"LevelCollection"))
                .Returns(ScriptableObject.CreateInstance<SOLevelCollection>());
            _mockResourceLoader
                .Setup(x => x.Load<SOLevelData>(@"LevelData/" + savedLevelData.CurrentLevelName))
                .Returns(ScriptableObject.CreateInstance<SOLevelData>());
            _mockResourceLoader
                .Setup(x => x.Load<SOCutsceneData>(@"CutsceneData/" + savedLevelData.CurrentCutsceneName))
                .Returns(ScriptableObject.CreateInstance<SOCutsceneData>());


            // Replace SaveSystem with Mock
            _controller.SetModel(_model);
            _controller.SetSaveSystem(_mockSaveSystem.Object);

            // Act
            var initialize = _controller.Initialize();
            while (initialize.MoveNext()) { }

            // Assert
            Assert.AreEqual(savedLevelData.CurrentLevelName, _model.SavedLevelData.CurrentLevelName);
            Assert.AreEqual(savedLevelData.CurrentCutsceneName, _model.SavedLevelData.CurrentCutsceneName);
        }

        [Test]
        public void OnChooseLevel_ShouldSetLevelAndCutsceneAndSaveData()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            levelData.name = "Level1";
            levelData.CutsceneData = ScriptableObject.CreateInstance<SOCutsceneData>();
            levelData.CutsceneData.name = "Cutscene1";
            var message = new ChooseLevelMessage(levelData);

            _mockResourceLoader
                .Setup(x => x.Load<SOLevelData>(@"LevelData/" + levelData.name))
                .Returns(levelData);
            _mockResourceLoader
                .Setup(x => x.Load<SOCutsceneData>(@"CutsceneData/" + levelData.CutsceneData.name))
                .Returns(levelData.CutsceneData);

            // Act
            _controller.OnChooseLevel(message);

            // Assert
            Assert.AreEqual(levelData, _model.CurrentLevelData);
            Assert.AreEqual(levelData.CutsceneData, _model.CurrentCutsceneData);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedLevelData>(y => y.CurrentLevelName == "Level1" && y.CurrentCutsceneName == "Cutscene1")), Times.Once);
        }

        [Test]
        public void OnUnlockLevel_ShouldAddLevelAndSaveData()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            levelData.name = "Level1";
            var message = new UnlockLevelMessage(levelData);

            // Act
            _controller.OnUnlockLevel(message);

            // Assert
            Assert.Contains("Level1", _model.SavedLevelData.UnlockedLevels);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedLevelData>(y => y.UnlockedLevels.Contains("Level1"))), Times.Once);
        }

        [Test]
        public void OnDeleteSaveData_ShouldDeleteSavedData()
        {
            // Arrange
            _mockSaveSystem.Setup(x => x.Delete());

            // Act
            _controller.OnDeleteSaveData(new DeleteSaveDataMessage());

            // Assert
            _mockSaveSystem.Verify(x => x.Delete(), Times.Once);
        }
    }
}