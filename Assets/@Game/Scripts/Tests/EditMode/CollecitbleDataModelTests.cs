using NUnit.Framework;
using ProjectTA.Module.CollectibleData;
using System.Collections.Generic;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    public class CollectibleDataModelTests
    {
        private CollectibleDataModel _collectibleDataModel;

        [SetUp]
        public void Setup()
        {
            _collectibleDataModel = new CollectibleDataModel();
        }

        [Test]
        public void SetCollectibleCollection_ShouldSetCollectibleCollectionCorrectly()
        {
            // Arrange
            var collectibleCollection = ScriptableObject.CreateInstance<SOCollectibleCollection>();

            // Use reflection to set the private field
            var fieldInfo = typeof(SOCollectibleCollection).GetField("_collectibleItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(collectibleCollection, new List<SOCollectibleData>
            {
                ScriptableObject.CreateInstance<SOCollectibleData>(),
                ScriptableObject.CreateInstance<SOCollectibleData>()
            });

            // Act
            _collectibleDataModel.SetCollectibleCollection(collectibleCollection);

            // Assert
            Assert.AreEqual(collectibleCollection, _collectibleDataModel.CollectibleCollection);
        }

        [Test]
        public void AddUnlockedCollectibleCollection_ShouldAddCollectibleToUnlockedList()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "TestCollectible";

            // Act
            _collectibleDataModel.AddUnlockedCollectibleCollection(collectibleData);

            // Assert
            Assert.IsTrue(_collectibleDataModel.UnlockedCollectibleItems.Contains(collectibleData));
        }

        [Test]
        public void AddUnlockedCollectibleCollection_ShouldNotAddDuplicateCollectible()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "TestCollectible";

            // Act
            _collectibleDataModel.AddUnlockedCollectibleCollection(collectibleData);
            _collectibleDataModel.AddUnlockedCollectibleCollection(collectibleData); // Adding again

            // Assert
            Assert.AreEqual(1, _collectibleDataModel.UnlockedCollectibleItems.Count);
        }

        [Test]
        public void AddUnlockedCollectibleCollection_ShouldLogWarningIfAlreadyUnlocked()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "TestCollectible";

            // Act
            _collectibleDataModel.AddUnlockedCollectibleCollection(collectibleData);
            var logMessage = $"TestCollectible is already unlocked!";
            LogAssert.Expect(LogType.Warning, logMessage);
            _collectibleDataModel.AddUnlockedCollectibleCollection(collectibleData); // Adding again
        }

        [Test, Performance]
        public void Performance_AddUnlockedCollectible()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "PerformanceCollectible";

            Measure.Method(() =>
            {
                // Act
                _collectibleDataModel.AddUnlockedCollectibleCollection(collectibleData);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.IsTrue(_collectibleDataModel.UnlockedCollectibleItems.Contains(collectibleData));
        }

        [Test, Performance]
        public void Performance_SetCollectibleCollection()
        {
            // Arrange
            var collectibleCollection = ScriptableObject.CreateInstance<SOCollectibleCollection>();

            // Use reflection to set the private field
            var fieldInfo = typeof(SOCollectibleCollection).GetField("_collectibleItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(collectibleCollection, new List<SOCollectibleData>
            {
                ScriptableObject.CreateInstance<SOCollectibleData>(),
                ScriptableObject.CreateInstance<SOCollectibleData>()
            });

            Measure.Method(() =>
            {
                // Act
                _collectibleDataModel.SetCollectibleCollection(collectibleCollection);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(collectibleCollection, _collectibleDataModel.CollectibleCollection);
        }
    }
}