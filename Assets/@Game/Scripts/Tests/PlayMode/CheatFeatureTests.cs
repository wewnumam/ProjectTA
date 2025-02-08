using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CheatFeature;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class CheatFeatureTests
    {
        private CheatFeatureController _controller;
        private CheatFeatureView _view;
        private CheatFeatureModel _model;
        private Mock<IGameConstantsModel> _mockGameConstantsModel;

        [SetUp]
        public void SetUp()
        {
            // Initialize controller, model, and view
            _controller = new CheatFeatureController();
            _model = new CheatFeatureModel();
            _view = new GameObject().AddComponent<CheatFeatureView>();

            // Mock dependencies
            var gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();
            TestUtility.SetPrivateField(gameConstants, "_isJoystickActive", true);
            _mockGameConstantsModel = new Mock<IGameConstantsModel>();
            _mockGameConstantsModel.Setup(gcm => gcm.GameConstants).Returns(gameConstants);

            // Set up UI components for the view using TestUtility.SetPrivateField
            TestUtility.SetPrivateField(_view, "_deleteSaveDataButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_activateJoystickToggle", new GameObject().AddComponent<Toggle>());
            TestUtility.SetPrivateField(_view, "_gameWinButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_gameOverButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_addHealthButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_subtractHealthButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_addCollectedPuzzlePieceCountButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_subtractCollectedPuzzlePieceCountButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_addCollectedHiddenObjectCountButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_subtractCollectedHiddenObjectCountButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_addKillCountButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_subtractKillCountButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_teleportToPuzzleButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_teleportToCollectibleButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_blurCameraButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_normalCameraButon", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_restartCountdownButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_resetCountdownButton", new GameObject().AddComponent<Button>());

            // Initialize controller with model and view
            _controller.SetModel(_model);
            var obj = new GameObject();
            obj.tag = TagManager.TAG_PLAYER;
            _controller.SetView(_view);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up objects
            Object.DestroyImmediate(_view.gameObject);
            _controller = null;
            _model = null;
            _mockGameConstantsModel = null;
        }

        #region Controller Tests

        [Test]
        public void InitModel_WithValidData_SetsJoystickActive()
        {
            // Act
            _controller.InitModel(_mockGameConstantsModel.Object);

            // Assert
            Assert.IsTrue(_model.IsJoystickActive);
        }

        [Test]
        public void OnDeleteSaveData_PublishesDeleteSaveDataMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnDeleteSaveData", null);


            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<DeleteSaveDataMessage>(_controller.PublishedMessages.First());
        }

        [Test]
        public void OnActivateJoystick_PublishesActivateJoystickMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnActivateJoystick", new object[] { true });

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (ActivateJoystickMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.IsTrue(message.IsJoystickActive);
        }

        [Test]
        public void OnGameWin_PublishesGameWinMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnGameWin", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<GameWinMessage>(_controller.PublishedMessages.First());
        }

        [Test]
        public void OnGameOver_PublishesGameOverMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnGameOver", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<GameOverMessage>(_controller.PublishedMessages.First());
        }

        [Test]
        public void OnAddHealth_PublishesAdjustHealthCountMessageWithPositiveValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnAddHealth", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustHealthCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(1, message.Amount);
        }

        [Test]
        public void OnSubtractHealth_PublishesAdjustHealthCountMessageWithNegativeValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnSubtractHealth", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustHealthCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(-1, message.Amount);
        }

        [Test]
        public void OnAddPuzzlePieceCount_PublishesAdjustCollectedPuzzlePieceCountMessageWithPositiveValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnAddPuzzlePieceCount", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustCollectedPuzzlePieceCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(1, message.Amount);
        }

        [Test]
        public void OnSubtractPuzzlePieceCount_PublishesAdjustCollectedPuzzlePieceCountMessageWithNegativeValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnSubtractPuzzlePieceCount", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustCollectedPuzzlePieceCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(-1, message.Amount);
        }

        [Test]
        public void OnAddHiddenObjectCount_PublishesAdjustCollectedHiddenObjectCountMessageWithPositiveValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnAddHiddenObjectCount", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustCollectedHiddenObjectCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(1, message.Amount);
        }

        [Test]
        public void OnSubtractHiddenObjectCount_PublishesAdjustCollectedHiddenObjectCountMessageWithNegativeValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnSubtractHiddenObjectCount", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustCollectedHiddenObjectCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(-1, message.Amount);
        }

        [Test]
        public void OnAddKillCount_PublishesAdjustKillCountMessageWithPositiveValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnAddKillCount", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustKillCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(1, message.Amount);
        }

        [Test]
        public void OnSubtractKillCount_PublishesAdjustKillCountMessageWithNegativeValue()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnSubtractKillCount", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            var message = (AdjustKillCountMessage)_controller.PublishedMessages.First();
            Assert.IsNotNull(message);
            Assert.AreEqual(-1, message.Amount);
        }

        [Test]
        public void OnTeleportToPuzzle_CallsTeleportToPuzzleOnModel()
        {
            // Arrange
            var puzzle1 = new GameObject().transform;
            var puzzle2 = new GameObject().transform;
            TestUtility.GetPrivateField<List<Transform>>(_model, "_puzzleTransforms").Add(puzzle1);
            TestUtility.GetPrivateField<List<Transform>>(_model, "_puzzleTransforms").Add(puzzle2);
            var player = new GameObject().transform;
            _model.SetPlayerCharacter(player);

            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnTeleportToPuzzle", null);

            // Assert
            Assert.AreEqual(puzzle1.position, player.position);
        }

        [Test]
        public void OnTeleportToHiddenObject_CallsTeleportToHiddenObjectOnModel()
        {
            // Arrange
            var hiddenObject1 = new GameObject().transform;
            var hiddenObject2 = new GameObject().transform;
            TestUtility.GetPrivateField<List<Transform>>(_model, "_hiddenObjectTransforms").Add(hiddenObject1);
            TestUtility.GetPrivateField<List<Transform>>(_model, "_hiddenObjectTransforms").Add(hiddenObject2);
            var player = new GameObject().transform;
            _model.SetPlayerCharacter(player);

            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnTeleportToHiddenObject", null);

            // Assert
            Assert.AreEqual(hiddenObject1.position, player.position);
        }

        [Test]
        public void OnBlurCamera_PublishesCameraBlurMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnBlurCamera", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<CameraBlurMessage>(_controller.PublishedMessages.First());
        }

        [Test]
        public void OnNormalCamera_PublishesCameraNormalMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnNormalCamera", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<CameraNormalMessage>(_controller.PublishedMessages.First());
        }

        [Test]
        public void OnRestartCountdown_PublishesCountdownRestartMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnRestartCountdown", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<CountdownRestartMessage>(_controller.PublishedMessages.First());
        }

        [Test]
        public void OnResetCountdown_PublishesCountdownResetMessage()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnResetCountdown", null);

            // Assert
            Assert.AreEqual(1, _controller.PublishedMessages.Count());
            Assert.IsInstanceOf<CountdownResetMessage>(_controller.PublishedMessages.First());
        }

        #endregion

        #region View Tests

        [Test]
        public void SetUtilityCallbacks_AttachesListenersToDeleteSaveDataButton()
        {
            // Arrange
            bool callbackInvoked = false;
            UnityAction callback = () => callbackInvoked = true;

            // Act
            _view.SetUtilityCallbacks(callback, null);
            TestUtility.GetPrivateField<Button>(_view, "_deleteSaveDataButton").onClick.Invoke();

            // Assert
            Assert.IsTrue(callbackInvoked);
        }

        [Test]
        public void SetUtilityCallbacks_AttachesListenersToActivateJoystickToggle()
        {
            // Arrange
            bool callbackInvoked = false;
            UnityAction<bool> callback = (isActive) => callbackInvoked = isActive;

            // Act
            _view.SetUtilityCallbacks(null, callback);
            _view.ActivateJoystickToggle.isOn = true;
            _view.ActivateJoystickToggle.onValueChanged.Invoke(true);

            // Assert
            Assert.IsTrue(callbackInvoked);
        }

        #endregion

        #region Model Tests

        [Test]
        public void SetIsJoystickActive_UpdatesIsJoystickActiveProperty()
        {
            // Act
            _model.SetIsJoystickActive(true);

            // Assert
            Assert.IsTrue(_model.IsJoystickActive);
        }

        #endregion
    }
}