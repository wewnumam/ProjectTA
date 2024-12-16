using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using UnityEditor;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class CollectibleDataControllerTests
    {
        private CollectibleDataController _controller;

        private const string COLLECTIBLE_DATA_NAME = "CollectibleDataTest";
        private const string COLLECTIBLE_COLLECTION_NAME = "CollectibleCollectionTest";

        [SetUp]
        public void SetUp()
        {
            // Create the ScriptableObject instance
            var collectibleCollection = ScriptableObject.CreateInstance<SOCollectibleCollection>();

            // Use reflection to set the private field
            var fieldInfo = typeof(SOCollectibleCollection).GetField("_collectibleItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(collectibleCollection, new List<SOCollectibleData> { ScriptableObject.CreateInstance<SOCollectibleData>() });

            AssetDatabase.CreateAsset(collectibleCollection, $"Assets/Resources/{COLLECTIBLE_COLLECTION_NAME}.asset");

            // Create a ScriptableObject file for CollectibleData
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            AssetDatabase.CreateAsset(collectibleData, $"Assets/Resources/CollectibleData/{COLLECTIBLE_DATA_NAME}.asset");

            var model = new CollectibleDataModel();
            model.SetCollectibleCollection(collectibleCollection);
            model.AddUnlockedCollectibleCollection(collectibleData);

            _controller = new CollectibleDataController();
            _controller.SetModel(model);
        }

        [TearDown]
        public void TearDown()
        {
            // Delete the ScriptableObject files
            AssetDatabase.DeleteAsset($"Assets/Resources/{COLLECTIBLE_COLLECTION_NAME}.asset");
            AssetDatabase.DeleteAsset($"Assets/Resources/CollectibleData/{COLLECTIBLE_DATA_NAME}.asset");
        }

        [Test]
        public void AddUnlockedCollectible_CorrectlyAddsCollectibleToModel()
        {
            // Act
            _controller.AddUnlockedCollectible(COLLECTIBLE_DATA_NAME);

            // Assert
            Assert.IsTrue(_controller.Model.UnlockedCollectibleItems.Contains(Resources.Load<SOCollectibleData>(@"CollectibleData/" + COLLECTIBLE_DATA_NAME)));
        }

        [Test]
        public void Initialize_CorrectlySetsCollectibleCollection()
        {
            // Act
            _controller.Initialize();

            // Assert
            Assert.AreEqual(Resources.Load<SOCollectibleCollection>(COLLECTIBLE_COLLECTION_NAME), _controller.Model.CollectibleCollection);
        }

        [Test]
        public void OnUnlockCollectible_CorrectlyAddsCollectibleToModel()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            var message = new UnlockCollectibleMessage(collectibleData);

            // Act
            _controller.OnUnlockCollectible(message);

            // Assert
            Assert.IsTrue(_controller.Model.UnlockedCollectibleItems.Contains(collectibleData));
        }

        [Test, Performance]
        public void Performance_AddUnlockedCollectible()
        {
            Measure.Method(() =>
            {
                _controller.AddUnlockedCollectible(COLLECTIBLE_DATA_NAME);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert after performance test
            Assert.IsTrue(_controller.Model.UnlockedCollectibleItems.Contains(Resources.Load<SOCollectibleData>(@"CollectibleData/" + COLLECTIBLE_DATA_NAME)));
        }

        [Test, Performance]
        public void Performance_Initialize()
        {
            Measure.Method(() =>
            {
                _controller.Initialize();
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert after performance test
            Assert.AreEqual(Resources.Load<SOCollectibleCollection>(COLLECTIBLE_COLLECTION_NAME), _controller.Model.CollectibleCollection);
        }

        [Test, Performance]
        public void Performance_OnUnlockCollectible()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            var message = new UnlockCollectibleMessage(collectibleData);

            Measure.Method(() =>
            {
                _controller.OnUnlockCollectible(message);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert after performance test
            Assert.IsTrue(_controller.Model.UnlockedCollectibleItems.Contains(collectibleData));
        }
    }
}