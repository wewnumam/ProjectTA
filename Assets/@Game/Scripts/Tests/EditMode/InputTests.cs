using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.Input;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTA.Tests
{
    public class InputTests
    {
        private InputController _inputController;

        [SetUp]
        public void SetUp()
        {
            // Initialize the InputController before each test
            _inputController = new InputController();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up after each test
            _inputController = null;
        }

        [Test]
        public void Initialize_EnablesInputActionsAndSubscribesToEvents()
        {
            // Act
            var initialize = _inputController.Initialize();
            while (initialize.MoveNext()) { }

            // Assert
            Assert.IsNotNull(_inputController.InputActions);
            Assert.IsTrue(_inputController.InputActions.Character.enabled);

            // Verify subscriptions (mocking or reflection would be required for full verification)
        }

        [Test]
        public void Terminate_DisablesInputActionsAndUnsubscribesFromEvents()
        {
            // Arrange
            var initialize = _inputController.Initialize();
            while (initialize.MoveNext()) { }

            // Act
            var terminate = _inputController.Terminate();
            while (terminate.MoveNext()) { }

            // Assert
            Assert.IsFalse(_inputController.InputActions.Character.enabled);

            // Verify unsubscriptions (mocking or reflection would be required for full verification)
        }

        [Test]
        public void OnStartMove_PublishesMovePlayerCharacterMessage()
        {
            // Arrange
            var mockContext = new Mock<IInputActionCallbackContext>();
            mockContext.Setup(ctx => ctx.ReadValue()).Returns(new Vector2(1f, 1f));

            // Act
            _inputController.StartMove(mockContext.Object);

            // Assert
            Assert.AreEqual(1, _inputController.PublishedMessages.Count());
            var message = (MovePlayerCharacterMessage)_inputController.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(new Vector2(1f, 1f), message.Direction);
        }

        [Test]
        public void OnEndMove_PublishesMovePlayerCharacterMessageWithZeroVector()
        {
            // Arrange
            var context = new InputAction.CallbackContext();

            // Act
            _inputController.OnEndMove(context);

            // Assert
            Assert.AreEqual(1, _inputController.PublishedMessages.Count());
            var message = (MovePlayerCharacterMessage)_inputController.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(Vector2.zero, message.Direction);
        }

        [Test]
        public void OnShootStart_PublishesPlayerCharacterShootStartMessage()
        {
            // Arrange
            var context = new InputAction.CallbackContext();

            // Act
            _inputController.OnShootStart(context);

            // Assert
            Assert.AreEqual(1, _inputController.PublishedMessages.Count());
            var message = (PlayerCharacterShootStartMessage)_inputController.PublishedMessages.First();
            Assert.IsNotNull(message);
        }

        [Test]
        public void OnShootEnd_PublishesPlayerCharacterShootEndMessage()
        {
            // Arrange
            var context = new InputAction.CallbackContext();

            // Act
            _inputController.OnShootEnd(context);

            // Assert
            Assert.AreEqual(1, _inputController.PublishedMessages.Count());
            var message = (PlayerCharacterShootEndMessage)_inputController.PublishedMessages.First();
            Assert.IsNotNull(message);
        }

        [Test]
        public void OnStartAim_PublishesRotatePlayerCharacterMessage()
        {
            // Arrange
            var mockContext = new Mock<IInputActionCallbackContext>();
            mockContext.Setup(ctx => ctx.ReadValue()).Returns(new Vector2(1f, 1f));

            // Act
            _inputController.StartAim(mockContext.Object);

            // Assert
            Assert.AreEqual(1, _inputController.PublishedMessages.Count());
            var message = (RotatePlayerCharacterMessage)_inputController.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(new Vector2(1f, 1f), message.Aim);
        }

        [Test]
        public void OnEndAim_PublishesRotatePlayerCharacterMessageWithZeroVector()
        {
            // Arrange
            var context = new InputAction.CallbackContext();

            // Act
            _inputController.OnEndAim(context);

            // Assert
            Assert.AreEqual(1, _inputController.PublishedMessages.Count());
            var message = (RotatePlayerCharacterMessage)_inputController.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(Vector2.zero, message.Aim);
        }
    }
}