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
    }
}