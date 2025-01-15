using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class SaveSystemControllerTests
    {
        private SaveSystemController _controller;

        private const string SAVE_DATA_NAME = "SaveDataTest";

        [SetUp]
        public void SetUp()
        {
            // Create the SaveSystemController instance
            _controller = new SaveSystemController();
            _controller.SetModel(new SaveSystemModel());
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up any created assets if necessary
            _controller.DeleteSaveFile();
        }

        [Test]
        public void Initialize_ShouldLoadSaveData()
        {
            // Act
            _controller.Initialize();

            // Assert
            Assert.IsNotNull(_controller.Model.SaveData);
            Assert.AreEqual(TagManager.DEFAULT_CUTSCENENAME, _controller.Model.SaveData.CurrentCutsceneName);
            Assert.AreEqual(TagManager.DEFAULT_LEVELNAME, _controller.Model.SaveData.CurrentLevelName);
        }

        [Test]
        public void SaveGame_ShouldSaveDataCorrectly()
        {
            // Arrange
            var saveData = new SaveData
            {
                CurrentCutsceneName = "Cutscene1",
                CurrentLevelName = "Level1",
                UnlockedLevels = new List<string> { "Level1", "Level2" },
                UnlockedCollectibles = new List<string> { "Collectible1" }
            };

            // Act
            _controller.SaveGame(saveData);

            // Assert
            Assert.AreEqual(saveData.CurrentCutsceneName, _controller.Model.SaveData.CurrentCutsceneName);
            Assert.AreEqual(saveData.CurrentLevelName, _controller.Model.SaveData.CurrentLevelName);
            Assert.AreEqual(saveData.UnlockedLevels, _controller.Model.SaveData.UnlockedLevels);
            Assert.AreEqual(saveData.UnlockedCollectibles, _controller.Model.SaveData.UnlockedCollectibles);
        }

        [Test]
        public void LoadGame_ShouldLoadDataCorrectly()
        {
            // Arrange
            var saveData = new SaveData
            {
                CurrentCutsceneName = "Cutscene1",
                CurrentLevelName = "Level1",
                UnlockedLevels = new List<string> { "Level1", "Level2" },
                UnlockedCollectibles = new List<string> { "Collectible1" }
            };

            _controller.SaveGame(saveData); // Save the data first

            // Act
            var loadedData = _controller.LoadGame();

            // Assert
            Assert.AreEqual(saveData.CurrentCutsceneName, loadedData.CurrentCutsceneName);
            Assert.AreEqual(saveData.CurrentLevelName, loadedData.CurrentLevelName);
            Assert.AreEqual(saveData.UnlockedLevels, loadedData.UnlockedLevels);
            Assert.AreEqual(saveData.UnlockedCollectibles, loadedData.UnlockedCollectibles);
        }

        [Test, Performance]
        public void Performance_SaveGame()
        {
            // Arrange
            var saveData = new SaveData
            {
                CurrentCutsceneName = "PerformanceCutscene",
                CurrentLevelName = "PerformanceLevel",
                UnlockedLevels = new List<string> { "Level1", "Level2" },
                UnlockedCollectibles = new List<string> { "Collectible1" }
            };

            Measure.Method(() =>
            {
                // Act
                _controller.SaveGame(saveData);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(saveData.CurrentCutsceneName, _controller.Model.SaveData.CurrentCutsceneName);
            Assert.AreEqual(saveData.CurrentLevelName, _controller.Model.SaveData.CurrentLevelName);
            Assert.AreEqual(saveData.UnlockedLevels, _controller.Model.SaveData.UnlockedLevels);
            Assert.AreEqual(saveData.UnlockedCollectibles, _controller.Model.SaveData.UnlockedCollectibles);
        }

        [Test, Performance]
        public void Performance_LoadGame()
        {
            // Arrange
            var saveData = new SaveData
            {
                CurrentCutsceneName = "PerformanceCutscene",
                CurrentLevelName = "PerformanceLevel",
                UnlockedLevels = new List<string> { "Level1", "Level2" },
                UnlockedCollectibles = new List<string> { "Collectible1" }
            };

            _controller.SaveGame(saveData); // Save the data first

            Measure.Method(() =>
            {
                // Act
                _controller.LoadGame();
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(saveData.CurrentCutsceneName, _controller.Model.SaveData.CurrentCutsceneName);
            Assert.AreEqual(saveData.CurrentLevelName, _controller.Model.SaveData.CurrentLevelName);
            Assert.AreEqual(saveData.UnlockedLevels, _controller.Model.SaveData.UnlockedLevels);
            Assert.AreEqual(saveData.UnlockedCollectibles, _controller.Model.SaveData.UnlockedCollectibles);
        }

        [Test]
        public void UnlockLevel_ShouldAddLevelToUnlockedLevels()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            levelData.name = "Level1";
            var saveData = new SaveData();
            var model = new SaveSystemModel();
            model.SetSaveData(saveData);
            _controller.SetModel(model);

            // Act
            _controller.UnlockLevel(new UnlockLevelMessage(levelData));

            // Assert
            Assert.IsTrue(_controller.Model.SaveData.UnlockedLevels.Contains(levelData.name));
        }

        [Test]
        public void UnlockCollectible_ShouldAddCollectibleToUnlockedCollectibles()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "Collectible1";
            var saveData = new SaveData();
            var model = new SaveSystemModel();
            model.SetSaveData(saveData);
            _controller.SetModel(model);

            // Act
            _controller.UnlockCollectible(new UnlockCollectibleMessage(collectibleData));

            // Assert
            Assert.IsTrue(_controller.Model.SaveData.UnlockedCollectibles.Contains(collectibleData.name));
        }
    }
}