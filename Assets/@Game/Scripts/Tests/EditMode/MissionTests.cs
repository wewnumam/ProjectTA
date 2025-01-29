using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.Mission;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class MissionTests
    {
        private MissionModel _model;
        private MissionController _controller;
        private SOLevelData _testLevelData;

        [SetUp]
        public void Setup()
        {
            _model = new MissionModel();
            _controller = new MissionController();
            _controller.SetModel(_model);

            // Setup test level data
            _testLevelData = ScriptableObject.CreateInstance<SOLevelData>();
            _testLevelData.PuzzleObjects = new List<PuzzleObject>(new PuzzleObject[2]); // Count = 2
            _testLevelData.HiddenObjects = new List<SOCollectibleData> { ScriptableObject.CreateInstance<SOCollectibleData>() }; // Count = 1
            _testLevelData.NextLevel = ScriptableObject.CreateInstance<SOLevelData>();
        }

        #region MissionModel Tests

        [Test]
        public void SetCurrentLevelData_ShouldSetCurrentAndNextLevelData()
        {
            // Act
            _model.SetCurrentLevelData(_testLevelData);

            // Assert
            Assert.AreEqual(_testLevelData, _model.CurrentLevelData);
            Assert.AreEqual(_testLevelData.NextLevel, _model.NextLevelData);
        }

        [Test]
        public void AdjustCollectedPuzzlePieceCount_ShouldClampBetweenZeroAndPuzzleCount()
        {
            // Arrange
            _model.SetPuzzleCount(3);

            // Act & Assert
            _model.AdjustCollectedPuzzlePieceCount(2);
            Assert.AreEqual(2, _model.CollectedPuzzlePieceCount);

            _model.AdjustCollectedPuzzlePieceCount(2);
            Assert.AreEqual(3, _model.CollectedPuzzlePieceCount);

            _model.AdjustCollectedPuzzlePieceCount(-5);
            Assert.AreEqual(0, _model.CollectedPuzzlePieceCount);
        }

        [Test]
        public void AdjustPadlockOnPlaceCount_ShouldNotExceedPuzzleCount()
        {
            // Arrange
            _model.SetPuzzleCount(2);

            // Act & Assert
            _model.AdjustPadlockOnPlaceCount(3);
            Assert.AreEqual(2, _model.PadlockOnPlaceCount);

            _model.AdjustPadlockOnPlaceCount(-3);
            Assert.AreEqual(0, _model.PadlockOnPlaceCount);
        }

        [Test]
        public void IsPuzzleCompleted_ShouldReturnTrueWhenPadlockReachesPuzzleCount()
        {
            // Arrange
            _model.SetPuzzleCount(2);

            // Act
            _model.AdjustPadlockOnPlaceCount(2);

            // Assert
            Assert.IsTrue(_model.IsPuzzleCompleted());
        }

        [Test]
        public void AdjustCollectedHiddenObjectCount_ShouldClampBetweenZeroAndHiddenCount()
        {
            // Arrange
            _model.SetHiddenObjectCount(3);

            // Act & Assert
            _model.AdjustCollectedHiddenObjectCount(2);
            Assert.AreEqual(2, _model.CollectedHiddenObjectCount);

            _model.AdjustCollectedHiddenObjectCount(2);
            Assert.AreEqual(3, _model.CollectedHiddenObjectCount);

            _model.AdjustCollectedHiddenObjectCount(-5);
            Assert.AreEqual(0, _model.CollectedHiddenObjectCount);
        }

        [Test]
        public void AdjustKillCount_ShouldNeverGoBelowZero()
        {
            // Act & Assert
            _model.AdjustKillCount(-5);
            Assert.AreEqual(0, _model.KillCount);

            _model.AdjustKillCount(3);
            Assert.AreEqual(3, _model.KillCount);

            _model.AdjustKillCount(-1);
            Assert.AreEqual(2, _model.KillCount);

            _model.AdjustKillCount(-10);
            Assert.AreEqual(0, _model.KillCount);
        }

        [Test]
        public void SetHiddenObjectCount_SetsValue()
        {
            // Act
            _model.SetHiddenObjectCount(5);

            // Assert
            Assert.AreEqual(5, _model.HiddenObjectCount);
        }

        [Test]
        public void SetCollectedHiddenObjectCount_SetsValue()
        {
            // Arrange
            _model.SetHiddenObjectCount(3);

            // Act
            _model.SetCollectedHiddenObjectCount(5);

            // Assert
            Assert.AreEqual(5, _model.CollectedHiddenObjectCount);
        }

        [Test]
        public void SetCollectedPuzzlePieceCount_SetsValue()
        {
            // Arrange
            _model.SetPuzzleCount(2);

            // Act
            _model.SetCollectedPuzzlePieceCount(5);

            // Assert
            Assert.AreEqual(5, _model.CollectedPuzzlePieceCount);
        }

        [Test]
        public void AdjustPadlockOnPlaceCount_ShouldClampToZeroWhenNegative()
        {
            // Arrange
            _model.SetPuzzleCount(2);
            _model.AdjustPadlockOnPlaceCount(1);

            // Act
            _model.AdjustPadlockOnPlaceCount(-3);

            // Assert
            Assert.AreEqual(0, _model.PadlockOnPlaceCount);
        }

        #endregion

        #region MissionController Tests

        [Test]
        public void Init_WithValidLevelData_SetsModelCountsCorrectly()
        {
            // Act
            _controller.InitModel(_testLevelData);

            // Assert
            Assert.AreEqual(2, _model.PuzzlePieceCount);
            Assert.AreEqual(1, _model.HiddenObjectCount);
            Assert.AreEqual(_testLevelData, _model.CurrentLevelData);
        }

        [Test]
        public void OnAdjustCollectedPuzzlePieceCountMessage_UpdatesModelCorrectly()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            var message = new AdjustCollectedPuzzlePieceCountMessage(1);

            // Act
            _controller.OnAdjustCollectedPuzzlePieceCount(message);

            // Assert
            Assert.AreEqual(1, _model.CollectedPuzzlePieceCount);
        }

        [Test]
        public void OnAdjustHiddenObjectCountMessage_UpdatesModelCorrectly()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            var message = new AdjustCollectedHiddenObjectCountMessage(1);

            // Act
            _controller.OnAdjustCollectedHiddenObjectCount(message);

            // Assert
            Assert.AreEqual(1, _model.CollectedHiddenObjectCount);
        }

        [Test]
        public void OnAdjustPadlockCount_CompletesPuzzle_SetsCompletionState()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            var message = new AdjustPadlockOnPlaceCountMessage(2);

            // Act
            _controller.OnAdjustPadlockOnPlaceCount(message);

            // Assert
            Assert.IsTrue(_model.IsPuzzleCompleted());
        }

        [Test]
        public void OnAdjustKillCount_ShouldNeverGoBelowZero()
        {
            // Arrange
            _model.AdjustKillCount(5);
            var message = new AdjustKillCountMessage(-10);

            // Act
            _controller.OnAdjustKillCount(message);

            // Assert
            Assert.AreEqual(0, _model.KillCount);
        }

        [Test]
        public void OnAdjustHiddenObjectCountMessage_ClampsToMax()
        {
            // Arrange
            _controller.InitModel(_testLevelData); // HiddenObjectCount = 1
            var message = new AdjustCollectedHiddenObjectCountMessage(5);

            // Act
            _controller.OnAdjustCollectedHiddenObjectCount(message);

            // Assert
            Assert.AreEqual(1, _model.CollectedHiddenObjectCount);
        }

        [Test]
        public void OnAdjustKillCountMessage_AdjustsCorrectly()
        {
            // Arrange
            var message1 = new AdjustKillCountMessage(3);
            var message2 = new AdjustKillCountMessage(-1);

            // Act
            _controller.OnAdjustKillCount(message1);
            Assert.AreEqual(3, _model.KillCount);

            // Act again
            _controller.OnAdjustKillCount(message2);

            // Assert
            Assert.AreEqual(2, _model.KillCount);
        }

        #endregion
    }
}