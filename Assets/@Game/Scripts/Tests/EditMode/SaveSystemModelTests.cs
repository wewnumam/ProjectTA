using NUnit.Framework;
using ProjectTA.Module.SaveSystem;
using Unity.PerformanceTesting;
using System.Collections.Generic;

namespace ProjectTA.Tests
{
    public class SaveSystemModelTests
    {
        private SaveSystemModel _saveSystemModel;

        [SetUp]
        public void Setup()
        {
            _saveSystemModel = new SaveSystemModel();
        }

        [Test]
        public void SetSaveData_ShouldSetSaveDataCorrectly()
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
            _saveSystemModel.SetSaveData(saveData);

            // Assert
            Assert.AreEqual(saveData, _saveSystemModel.SaveData);
        }

        [Test]
        public void SetCurrentCutsceneName_ShouldUpdateCurrentCutsceneName()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string newCutsceneName = "NewCutscene";

            // Act
            _saveSystemModel.SetCurrentCutsceneName(newCutsceneName);

            // Assert
            Assert.AreEqual(newCutsceneName, _saveSystemModel.SaveData.CurrentCutsceneName);
        }

        [Test]
        public void SetCurrentLevelName_ShouldUpdateCurrentLevelName()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string newLevelName = "NewLevel";

            // Act
            _saveSystemModel.SetCurrentLevelName(newLevelName);

            // Assert
            Assert.AreEqual(newLevelName, _saveSystemModel.SaveData.CurrentLevelName);
        }

        [Test]
        public void AddUnlockedLevel_ShouldAddLevelToUnlockedLevels()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string levelToUnlock = "Level1";

            // Act
            _saveSystemModel.AddUnlockedLevel(levelToUnlock);

            // Assert
            Assert.IsTrue(_saveSystemModel.SaveData.UnlockedLevels.Contains(levelToUnlock));
        }

        [Test]
        public void AddUnlockedCollectible_ShouldAddCollectibleToUnlockedCollectibles()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string collectibleToUnlock = "Collectible1";

            // Act
            _saveSystemModel.AddUnlockedCollectible(collectibleToUnlock);

            // Assert
            Assert.IsTrue(_saveSystemModel.SaveData.UnlockedCollectibles.Contains(collectibleToUnlock));
        }

        [Test, Performance]
        public void Performance_SetSaveData()
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
                _saveSystemModel.SetSaveData(saveData);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(saveData, _saveSystemModel.SaveData);
        }

        [Test, Performance]
        public void Performance_SetCurrentCutsceneName()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string newCutsceneName = "PerformanceCutscene";

            Measure.Method(() =>
            {
                // Act
                _saveSystemModel.SetCurrentCutsceneName(newCutsceneName);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(newCutsceneName, _saveSystemModel.SaveData.CurrentCutsceneName);
        }

        [Test, Performance]
        public void Performance_SetCurrentLevelName()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string newLevelName = "PerformanceLevel";

            Measure.Method(() =>
            {
                // Act
                _saveSystemModel.SetCurrentLevelName(newLevelName);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(newLevelName, _saveSystemModel.SaveData.CurrentLevelName);
        }

        [Test, Performance]
        public void Performance_AddUnlockedLevel()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string levelToUnlock = "PerformanceLevel";

            Measure.Method(() =>
            {
                // Act
                _saveSystemModel.AddUnlockedLevel(levelToUnlock);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.IsTrue(_saveSystemModel.SaveData.UnlockedLevels.Contains(levelToUnlock));
        }

        [Test, Performance]
        public void Performance_AddUnlockedCollectible()
        {
            // Arrange
            var saveData = new SaveData();
            _saveSystemModel.SetSaveData(saveData);
            string collectibleToUnlock = "PerformanceCollectible";

            Measure.Method(() =>
            {
                // Act
                _saveSystemModel.AddUnlockedCollectible(collectibleToUnlock);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.IsTrue(_saveSystemModel.SaveData.UnlockedCollectibles.Contains(collectibleToUnlock));
        }
    }
}