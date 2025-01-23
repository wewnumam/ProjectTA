using NUnit.Framework;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.Mission;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class MissionModelTests
    {
        private MissionModel _missionModel;

        [SetUp]
        public void Setup()
        {
            _missionModel = new MissionModel();
        }

        [Test]
        public void SetCurrentLevelData_ShouldSetCurrentLevelDataCorrectly()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();

            // Act
            _missionModel.SetCurrentLevelData(levelData);

            // Assert
            Assert.AreEqual(levelData, _missionModel.CurrentLevelData);
        }

        [Test]
        public void SetPuzzleCount_ShouldSetPuzzleCountCorrectly()
        {
            // Arrange
            int puzzleCount = 5;

            // Act
            _missionModel.SetPuzzleCount(puzzleCount);

            // Assert
            Assert.AreEqual(puzzleCount, _missionModel.PuzzlePieceCount);
        }

        [Test]
        public void AddCollectedPuzzlePieceCount_ShouldIncreaseCollectedPuzzlePieceCount()
        {
            // Arrange
            _missionModel.SetPuzzleCount(5);
            _missionModel.SetCollectedPuzzlePieceCount(0);

            // Act
            _missionModel.AddCollectedPuzzlePieceCount(1);

            // Assert
            Assert.AreEqual(1, _missionModel.CollectedPuzzlePieceCount);
        }

        [Test]
        public void SubtractCollectedPuzzlePieceCount_ShouldDecreaseCollectedPuzzlePieceCount()
        {
            // Arrange
            _missionModel.SetPuzzleCount(5);
            _missionModel.SetCollectedPuzzlePieceCount(3);

            // Act
            _missionModel.SubtractCollectedPuzzlePieceCount(1);

            // Assert
            Assert.AreEqual(2, _missionModel.CollectedPuzzlePieceCount);
        }

        [Test]
        public void IsPuzzleCompleted_ShouldReturnTrue_WhenAllPiecesCollected()
        {
            // Arrange
            _missionModel.SetPuzzleCount(3);
            _missionModel.AddPadlockOnPlaceCount(3);

            // Act
            bool isCompleted = _missionModel.IsPuzzleCompleted();

            // Assert
            Assert.IsTrue(isCompleted);
        }

        [Test]
        public void IsPuzzleCompleted_ShouldReturnFalse_WhenNotAllPiecesCollected()
        {
            // Arrange
            _missionModel.SetPuzzleCount(3);
            _missionModel.AddPadlockOnPlaceCount(2);

            // Act
            bool isCompleted = _missionModel.IsPuzzleCompleted();

            // Assert
            Assert.IsFalse(isCompleted);
        }
    }
}