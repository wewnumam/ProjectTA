using NUnit.Framework;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using UnityEditor;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class LevelDataControllerTests
    {
        private LevelDataController _controller;

        private const string CUTSCENE_DATA_NAME = "CutsceneDataTest";
        private const string LEVEL_DATA_NAME = "LevelDataTest";
        private const string LEVEL_COLLECTION_NAME = "LevelCollectionTest";

        [SetUp]
        public void SetUp()
        {
            // Create the ScriptableObject instance for LevelCollection
            var levelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();

            // Use reflection to set the private field
            var levelItems = typeof(SOLevelCollection).GetField("_levelItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            levelItems.SetValue(levelCollection, new List<SOLevelData> { ScriptableObject.CreateInstance<SOLevelData>() });

            AssetDatabase.CreateAsset(levelCollection, $"Assets/Resources/{LEVEL_COLLECTION_NAME}.asset");

            // Create a ScriptableObject file for LevelData
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();

            AssetDatabase.CreateAsset(levelData, $"Assets/Resources/LevelData/{LEVEL_DATA_NAME}.asset");

            // Create a ScriptableObject file for CutsceneData
            var cursceneData = ScriptableObject.CreateInstance<SOCutsceneData>();

            AssetDatabase.CreateAsset(cursceneData, $"Assets/Resources/CutsceneData/{CUTSCENE_DATA_NAME}.asset");

            var model = new LevelDataModel();
            model.SetLevelCollection(levelCollection);
            model.SetCurrentLevelData(levelData);
            model.SetCurrentCutsceneData(cursceneData);

            _controller = new LevelDataController();
            _controller.SetModel(model);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up any created assets if necessary
            AssetDatabase.DeleteAsset($"Assets/Resources/{LEVEL_COLLECTION_NAME}.asset");
            AssetDatabase.DeleteAsset($"Assets/Resources/LevelData/{LEVEL_DATA_NAME}.asset");
            AssetDatabase.DeleteAsset($"Assets/Resources/CutsceneData/{CUTSCENE_DATA_NAME}.asset");
        }

        [Test]
        public void SetCurrentLevel_ShouldSetCurrentLevelDataCorrectly()
        {
            // Act
            _controller.SetCurrentLevel(LEVEL_DATA_NAME);

            // Assert
            Assert.IsNotNull(_controller.Model.CurrentLevelData);
            Assert.AreEqual(Resources.Load<SOLevelData>(@"LevelData/" + LEVEL_DATA_NAME), _controller.Model.CurrentLevelData);
        }

        [Test]
        public void SetCurrentCutscene_ShouldSetCurrentCutsceneDataCorrectly()
        {
            // Act
            _controller.SetCurrentCutscene(CUTSCENE_DATA_NAME);

            // Assert
            Assert.IsNotNull(_controller.Model.CurrentCutsceneData);
            Assert.AreEqual(Resources.Load<SOCutsceneData>(@"CutsceneData/" + CUTSCENE_DATA_NAME), _controller.Model.CurrentCutsceneData);
        }

        [Test, Performance]
        public void Performance_SetCurrentLevel()
        {
            Measure.Method(() =>
            {
                // Act
                _controller.SetCurrentLevel(LEVEL_DATA_NAME);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.IsNotNull(_controller.Model.CurrentLevelData);
            Assert.AreEqual(Resources.Load<SOLevelData>(@"LevelData/" + LEVEL_DATA_NAME), _controller.Model.CurrentLevelData);
        }

        [Test, Performance]
        public void Performance_SetCurrentCutscene()
        {
            Measure.Method(() =>
            {
                // Act
                _controller.SetCurrentCutscene(CUTSCENE_DATA_NAME);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.IsNotNull(_controller.Model.CurrentCutsceneData);
            Assert.AreEqual(Resources.Load<SOCutsceneData>(@"CutsceneData/" + CUTSCENE_DATA_NAME), _controller.Model.CurrentCutsceneData);
        }

        [Test]
        public void Initialize_ShouldSetLevelCollection()
        {
            // Act
            _controller.Initialize();

            // Assert
            Assert.IsNotNull(_controller.Model.LevelCollection);
            Assert.AreEqual(1, _controller.Model.LevelCollection.LevelItems.Count);
        }

        [Test, Performance]
        public void Performance_Initialize()
        {
            Measure.Method(() =>
            {
                // Act
                _controller.Initialize();
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.IsNotNull(_controller.Model.LevelCollection);
        }
    }
}