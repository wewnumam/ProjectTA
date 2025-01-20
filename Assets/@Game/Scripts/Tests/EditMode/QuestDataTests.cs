using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.QuestData;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class QuestDataTests
    {
        private Mock<IResourceLoader> _mockResourceLoader;
        private Mock<ISaveSystem<SavedQuestData>> _mockSavedQuestData;
        private Mock<ISaveSystem<SavedUnlockedCollectibles>> _mockSavedUnlockedCollectibles;
        private QuestDataModel _model;
        private QuestDataController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockResourceLoader = new Mock<IResourceLoader>();
            _mockSavedQuestData = new Mock<ISaveSystem<SavedQuestData>>();
            _mockSavedUnlockedCollectibles = new Mock<ISaveSystem<SavedUnlockedCollectibles>>();
            _model = new QuestDataModel();
            _controller = new QuestDataController();

            // Inject dependencies
            _controller.SetModel(_model);
            _controller.SetSaveSystem(_mockSavedQuestData.Object, _mockSavedUnlockedCollectibles.Object);
            _controller.SetResourceLoader(_mockResourceLoader.Object);

            _model.SetCurrentQuestData(new SavedQuestData());
            _model.SetUnlockedCollectibles(new SavedUnlockedCollectibles());
        }

        [Test]
        public void SetCurrentQuestData_ShouldSetQuestData()
        {
            // Arrange
            var savedData = new SavedQuestData();
            _mockSavedQuestData.Setup(x => x.Load()).Returns(savedData);

            // Act
            _model.SetCurrentQuestData(savedData);

            // Assert
            Assert.AreEqual(savedData, _model.CurrentQuestData);
        }

        [Test]
        public void SetUnlockedCollectibles_ShouldSetCollectiblesData()
        {
            // Arrange
            var savedCollectibles = new SavedUnlockedCollectibles();
            _mockSavedUnlockedCollectibles.Setup(x => x.Load()).Returns(savedCollectibles);

            // Act
            _model.SetUnlockedCollectibles(savedCollectibles);

            // Assert
            Assert.AreEqual(savedCollectibles, _model.UnlockedCollectibles);
        }

        [Test]
        public void Initialize_ShouldLoadSavedDataAndQuestCollection()
        {
            // Arrange
            var savedQuestData = new SavedQuestData();
            var savedCollectibles = new SavedUnlockedCollectibles();
            var questCollection = ScriptableObject.CreateInstance<SOQuestCollection>();
            var collectibleCollection = ScriptableObject.CreateInstance<SOCollectibleCollection>();

            _model.SetCurrentQuestData(savedQuestData);

            _mockSavedQuestData.Setup(x => x.Load()).Returns(savedQuestData);
            _mockSavedUnlockedCollectibles.Setup(x => x.Load()).Returns(savedCollectibles);
            _mockResourceLoader
                .Setup(x => x.Load<SOQuestCollection>(TagManager.SO_QUESTCOLLECTION))
                .Returns(questCollection);
            _mockResourceLoader
                .Setup(x => x.Load<SOCollectibleCollection>(TagManager.SO_COLLECTIBLECOLLECTION))
                .Returns(collectibleCollection);

            // Act
            var initialize = _controller.Initialize();
            while (initialize.MoveNext()) { }

            // Assert
            Assert.AreEqual(savedQuestData, _model.CurrentQuestData);
            Assert.AreEqual(savedCollectibles, _model.UnlockedCollectibles);
            Assert.AreEqual(questCollection, _model.QuestCollection);
        }

        [Test]
        public void OnAddLevelPlayed_ShouldAddLevelAndSaveData()
        {
            // Arrange
            var levelName = "Level1";
            var message = new AddLevelPlayedMessage(levelName);

            // Act
            _controller.OnAddLevelPlayed(message);

            // Assert
            Assert.Contains(levelName, _model.CurrentQuestData.LevelPlayed);
            _mockSavedQuestData.Verify(x => x.Save(_model.CurrentQuestData), Times.Once);
        }

        [Test]
        public void OnGameOver_ShouldUpdateMinutesPlayedAndSaveData()
        {
            // Arrange
            var initialMinutes = 5;
            _model.CurrentSessionInMinutes = initialMinutes;
            var message = new GameOverMessage();

            // Act
            _controller.OnGameOver(message);

            // Assert
            Assert.AreEqual(initialMinutes, _model.CurrentQuestData.CurrentMinutesPlayedAmount);
            _mockSavedQuestData.Verify(x => x.Save(_model.CurrentQuestData), Times.Once);
        }

        [Test]
        public void OnGameWin_ShouldUpdateWinCountAndSaveData()
        {
            // Arrange
            var initialWinCount = _model.CurrentQuestData.CurrentGameWinAmount;
            var message = new GameWinMessage();

            // Act
            _controller.OnGameWin(message);

            // Assert
            Assert.AreEqual(initialWinCount + 1, _model.CurrentQuestData.CurrentGameWinAmount);
            _mockSavedQuestData.Verify(x => x.Save(_model.CurrentQuestData), Times.Once);
        }

        [Test]
        public void OnDeleteSaveData_ShouldDeleteSavedQuestData()
        {
            // Act
            _controller.OnDeleteSaveData(new DeleteSaveDataMessage());

            // Assert
            _mockSavedQuestData.Verify(x => x.Delete(), Times.Once);
        }
    }
}
