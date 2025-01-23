using Moq;
using NUnit.Framework;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class GameConstantsTests
    {
        private Mock<IResourceLoader> _mockResourceLoader;
        private GameConstantsController _controller;
        private GameConstantsModel _model;

        [SetUp]
        public void Setup()
        {
            _mockResourceLoader = new Mock<IResourceLoader>();
            _controller = new GameConstantsController();
            _model = new GameConstantsModel();

            _controller.SetModel(_model);
            _controller.SetResourceLoader(_mockResourceLoader.Object);
        }

        [Test]
        public void SetGameConstants_ShouldSetGameConstantsCorrectly()
        {
            // Arrange
            var gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();

            // Act
            _model.SetGameConstants(gameConstants);

            // Assert
            Assert.AreEqual(gameConstants, _model.GameConstants);
        }

        [Test]
        public void Initialize_ShouldLoadSOGameConstantsCorrectly()
        {
            // Arrange
            var gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();

            _mockResourceLoader
                .Setup(x => x.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS))
                .Returns(gameConstants);

            // Act
            var initialize = _controller.Initialize();
            while (initialize.MoveNext()) { }

            // Assert
            Assert.AreEqual(gameConstants, _model.GameConstants);
        }
    }
}