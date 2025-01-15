using NUnit.Framework;
using ProjectTA.Module.GameConstants;
using Unity.PerformanceTesting;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class GameConstantsTests
    {
        private GameConstantsModel _gameConstantsModel;

        [SetUp]
        public void Setup()
        {
            _gameConstantsModel = new GameConstantsModel();
        }

        [Test]
        public void SetGameConstants_ShouldSetGameConstantsCorrectly()
        {
            // Arrange
            var gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();

            // Act
            _gameConstantsModel.SetGameConstants(gameConstants);

            // Assert
            Assert.AreEqual(gameConstants, _gameConstantsModel.GameConstants);
        }

        [Test, Performance]
        public void Performance_SetGameConstants()
        {
            // Arrange
            var gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();

            Measure.Method(() =>
            {
                // Act
                _gameConstantsModel.SetGameConstants(gameConstants);
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(gameConstants, _gameConstantsModel.GameConstants);
        }
    }
}