using NUnit.Framework;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class LevelDataModelTests
    {
        private LevelDataModel _levelDataModel;

        [SetUp]
        public void Setup()
        {
            _levelDataModel = new LevelDataModel();
        }

        [Test]
        public void SetCurrentLevelData_ShouldSetCurrentLevelDataCorrectly()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();

            // Act
            _levelDataModel.SetCurrentLevelData(levelData);

            // Assert
            Assert.AreEqual(levelData, _levelDataModel.CurrentLevelData);
        }

        [Test]
        public void SetCurrentCutsceneData_ShouldSetCurrentCutsceneDataCorrectly()
        {
            // Arrange
            var cutsceneData = ScriptableObject.CreateInstance<SOCutsceneData>();

            // Act
            _levelDataModel.SetCurrentCutsceneData(cutsceneData);

            // Assert
            Assert.AreEqual(cutsceneData, _levelDataModel.CurrentCutsceneData);
        }

        [Test]
        public void SetLevelCollection_ShouldSetLevelCollectionCorrectly()
        {
            // Arrange
            var levelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();

            // Act
            _levelDataModel.SetLevelCollection(levelCollection);

            // Assert
            Assert.AreEqual(levelCollection, _levelDataModel.LevelCollection);
        }

        [Test]
        public void SetCurrentEnvironmentPrefab_ShouldSetCurrentEnvironmentPrefabCorrectly()
        {
            // Arrange
            var environmentPrefab = new GameObject();

            // Act
            _levelDataModel.SetCurrentEnvironmentPrefab(environmentPrefab);

            // Assert
            Assert.AreEqual(environmentPrefab, _levelDataModel.CurrentEnvironmentPrefab);
        }

        [Test, Performance]
        public void Performance_SetCurrentLevelData()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();

            Measure.Method(() =>
            {
                // Act
                _levelDataModel.SetCurrentLevelData(levelData);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(levelData, _levelDataModel.CurrentLevelData);
        }

        [Test, Performance]
        public void Performance_SetCurrentCutsceneData()
        {
            // Arrange
            var cutsceneData = ScriptableObject.CreateInstance<SOCutsceneData>();

            Measure.Method(() =>
            {
                // Act
                _levelDataModel.SetCurrentCutsceneData(cutsceneData);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(cutsceneData, _levelDataModel.CurrentCutsceneData);
        }

        [Test, Performance]
        public void Performance_SetLevelCollection()
        {
            // Arrange
            var levelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();

            Measure.Method(() =>
            {
                // Act
                _levelDataModel.SetLevelCollection(levelCollection);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(levelCollection, _levelDataModel.LevelCollection);
        }

        [Test, Performance]
        public void Performance_SetCurrentEnvironmentPrefab()
        {
            // Arrange
            var environmentPrefab = new GameObject();

            Measure.Method(() =>
            {
                // Act
                _levelDataModel.SetCurrentEnvironmentPrefab(environmentPrefab);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(environmentPrefab, _levelDataModel.CurrentEnvironmentPrefab);
        }
    }
}