using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEditor.Progress;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class CollectibleDataTests
    {
        private Mock<IResourceLoader> _mockResourceLoader;
        private Mock<ISaveSystem<SavedUnlockedCollectibles>> _mockSaveSystem;
        private CollectibleDataModel _model;
        private CollectibleDataController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockResourceLoader = new Mock<IResourceLoader>();
            _mockSaveSystem = new Mock<ISaveSystem<SavedUnlockedCollectibles>>();
            _model = new CollectibleDataModel();
            _controller = new CollectibleDataController();

            // Inject dependencies
            _controller.SetModel(_model);
            _controller.SetSaveSystem(_mockSaveSystem.Object);
            _controller.SetResourceLoader(_mockResourceLoader.Object);
        }

        [Test]
        public void SetCollectibleCollection_ShouldSetCollection()
        {
            // Arrange
            var collectibleCollection = ScriptableObject.CreateInstance<SOCollectibleCollection>();

            // Act
            _model.SetCollectibleCollection(collectibleCollection);

            // Assert
            Assert.AreEqual(collectibleCollection, _model.CollectibleCollection);
        }

        [Test]
        public void AddUnlockedCollectibleCollection_ShouldAddCollectible()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "Collectible1";

            // Act
            _model.AddUnlockedCollectibleCollection(collectibleData);

            // Assert
            Assert.Contains(collectibleData, _model.UnlockedCollectibleItems);
            Assert.Contains("Collectible1", _model.UnlockedCollectiblesName.Items);
        }

        [Test]
        public void AddUnlockedCollectibleCollection_ShouldNotAddDuplicate()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "Collectible1";

            // Act
            _model.AddUnlockedCollectibleCollection(collectibleData);

            // Assert
            Assert.AreEqual(1, _model.UnlockedCollectibleItems.Count);
        }

        [Test]
        public void Initialize_ShouldLoadCollectiblesFromSaveSystem()
        {
            // Arrange
            var savedCollectibles = new SavedUnlockedCollectibles();
            savedCollectibles.Items = new List<string> { "Collectible1", "Collectible2" };
            var collectible1 = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectible1.name = "Collectible1";
            var collectible2 = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectible2.name = "Collectible2";

            _model.SetUnlockedCollectiblesName(savedCollectibles);

            _mockSaveSystem.Setup(x => x.Load()).Returns(savedCollectibles);
            _mockResourceLoader
                    .Setup(x => x.Load<SOCollectibleData>("CollectibleData/Collectible1"))
                    .Returns(collectible1);
            _mockResourceLoader
                    .Setup(x => x.Load<SOCollectibleData>("CollectibleData/Collectible2"))
                    .Returns(collectible2);
            _mockResourceLoader
                .Setup(x => x.Load<SOCollectibleCollection>(TagManager.SO_COLLECTIBLECOLLECTION))
                .Returns(ScriptableObject.CreateInstance<SOCollectibleCollection>());

            // Replace SaveSystem with Mock
            _controller.SetModel(_model);
            _controller.SetSaveSystem(_mockSaveSystem.Object);

            // Act
            var initialize = _controller.Initialize();
            while (initialize.MoveNext()) { }

            // Assert
            Assert.AreEqual(savedCollectibles.Items, _model.UnlockedCollectiblesName.Items);
        }

        [Test]
        public void OnUnlockCollectible_ShouldAddAndSaveCollectible()
        {
            // Arrange
            var collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            collectibleData.name = "Collectible1";
            var message = new UnlockCollectibleMessage(collectibleData);

            // Act
            _controller.OnUnlockCollectible(message);

            // Assert
            Assert.Contains(collectibleData, _model.UnlockedCollectibleItems);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedUnlockedCollectibles>(y => y.Items.Contains("Collectible1"))), Times.Once);
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