using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.Mission;
using Unity.PerformanceTesting;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class MissionControllerTests
    {
        private MissionController _controller;
        private MissionModel _model;

        private const string LEVEL_DATA_NAME = "LevelDataTest";

        [SetUp]
        public void SetUp()
        {
            // Create the model instance
            _model = new MissionModel();

            // Create the controller instance
            _controller = new MissionController();
            _controller.SetModel(_model);
        }

        [Test]
        public void SetCurrentLevelData_ShouldSetCurrentLevelDataCorrectly()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            levelData.name = LEVEL_DATA_NAME;

            // Act
            _controller.SetCurrentLevelData(levelData);

            // Assert
            Assert.AreEqual(levelData, _model.CurrentLevelData);
        }

        [Test]
        public void SetPuzzlePieceCount_ShouldSetPuzzlePieceCountCorrectly()
        {
            // Arrange
            int puzzleCount = 5;

            // Act
            _controller.SetPuzzlePieceCount(puzzleCount);

            // Assert
            Assert.AreEqual(puzzleCount, _model.PuzzlePieceCount);
        }

        [Test]
        public void AddCollectedPuzzlePiece_ShouldIncreaseCollectedPuzzlePieceCount()
        {
            // Arrange
            _model.SetPuzzleCount(5);
            _model.SetCollectedPuzzlePieceCount(0);

            // Act
            _controller.OnAddCollectedPuzzlePiece(new AddCollectedPuzzlePieceCountMessage(1));

            // Assert
            Assert.AreEqual(1, _model.CollectedPuzzlePieceCount);
        }

        [Test]
        public void SubtractCollectedPuzzlePiece_ShouldDecreaseCollectedPuzzlePieceCount()
        {
            // Arrange
            _model.SetPuzzleCount(5);
            _model.SetCollectedPuzzlePieceCount(3);

            // Act
            _controller.OnSubtractCollectedPuzzlePiece(new SubtractCollectedPuzzlePieceCountMessage(1));

            // Assert
            Assert.AreEqual(2, _model.CollectedPuzzlePieceCount);
        }

        [Test]
        public void OnAddPadlockOnPlace_ShouldIncreasePadlockCountAndCheckCompletion()
        {
            // Arrange
            _model.SetPuzzleCount(3);
            _model.SetCollectedPuzzlePieceCount(2);
            _model.SetCurrentLevelData(ScriptableObject.CreateInstance<SOLevelData>());

            // Act
            _controller.OnAddPadlockOnPlace(new AddPadlockOnPlaceMessage(1));

            // Assert
            Assert.AreEqual(1, _model.PadlockOnPlaceCount);
            Assert.IsFalse(_model.IsPuzzleCompleted()); // Not completed yet
            _controller.OnAddPadlockOnPlace(new AddPadlockOnPlaceMessage(2)); // Add more to complete
            Assert.IsTrue(_model.IsPuzzleCompleted()); // Now completed
        }

        [Test, Performance]
        public void Performance_SetCurrentLevelData()
        {
            // Arrange
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();

            Measure.Method(() =>
            {
                // Act
                _controller.SetCurrentLevelData(levelData);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(levelData, _model.CurrentLevelData);
        }

        [Test, Performance]
        public void Performance_AddCollectedPuzzlePiece()
        {
            // Arrange
            _model.SetPuzzleCount(120);
            _model.SetCollectedPuzzlePieceCount(0);

            Measure.Method(() =>
            {
                // Act
                _controller.OnAddCollectedPuzzlePiece(new AddCollectedPuzzlePieceCountMessage(1));
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(110, _model.CollectedPuzzlePieceCount);
        }

        [Test, Performance]
        public void Performance_SubtractCollectedPuzzlePiece()
        {
            // Arrange
            _model.SetPuzzleCount(120);
            _model.SetCollectedPuzzlePieceCount(120);

            Measure.Method(() =>
            {
                // Act
                _controller.OnSubtractCollectedPuzzlePiece(new SubtractCollectedPuzzlePieceCountMessage(1));
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(10, _model.CollectedPuzzlePieceCount);
        }
    }
}