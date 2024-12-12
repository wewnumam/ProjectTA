using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using System.Collections.Generic;
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
            // Create a ScriptableObject file for CollectibleCollection
            var collectibleCollection = ScriptableObject.CreateInstance<SO_CollectibleCollection>();
            collectibleCollection.CollectibleItems = new List<SO_CollectibleData> { ScriptableObject.CreateInstance<SO_CollectibleData>() };
            AssetDatabase.CreateAsset(collectibleCollection, $"Assets/Resources/{COLLECTIBLE_COLLECTION_NAME}.asset");

            // Create a ScriptableObject file for CollectibleData
            var collectibleData = ScriptableObject.CreateInstance<SO_CollectibleData>();
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
            Assert.IsTrue(_controller.Model.UnlockedCollectibleItems.Contains(Resources.Load<SO_CollectibleData>(@"CollectibleData/" + COLLECTIBLE_DATA_NAME)));
        }

        [Test]
        public void Initialize_CorrectlySetsCollectibleCollection()
        {
            // Act
            _controller.Initialize();

            // Assert
            Assert.AreEqual(Resources.Load<SO_CollectibleCollection>(COLLECTIBLE_COLLECTION_NAME), _controller.Model.CollectibleCollection);
        }

        [Test]
        public void OnUnlockCollectible_CorrectlyAddsCollectibleToModel()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SO_CollectibleData>();
            var message = new UnlockCollectibleMessage(collectibleData);

            // Act
            _controller.OnUnlockCollectible(message);

            // Assert
            Assert.IsTrue(_controller.Model.UnlockedCollectibleItems.Contains(collectibleData));
        }
    }
}