using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class GameSettingsTests
    {
        private Mock<ISaveSystem<SavedSettingsData>> _mockSaveSystem;
        private GameSettingsModel _model;
        private GameSettingsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockSaveSystem = new Mock<ISaveSystem<SavedSettingsData>>();
            _model = new GameSettingsModel();
            _controller = new GameSettingsController();

            // Inject dependencies
            _controller.SetModel(_model);
            _controller.SetSaveSystem(_mockSaveSystem.Object);
        }

        [Test]
        public void SetSaveData_ShouldUpdateSavedSettingsData()
        {
            // Arrange
            var saveData = new SavedSettingsData { IsBgmOn = true, IsSfxOn = false, IsVibrationOn = true, IsGameInductionActive = false };

            // Act
            _model.SetSaveData(saveData);

            // Assert
            Assert.AreEqual(saveData, _model.SavedSettingsData);
        }

        [Test]
        public void SetIsGameInductionActive_ShouldUpdateProperty()
        {
            // Act
            _model.SetIsGameIndctionActive(true);

            // Assert
            Assert.IsTrue(_model.SavedSettingsData.IsGameInductionActive);
        }

        [Test]
        public void SetIsSfxOn_ShouldUpdateProperty()
        {
            // Act
            _model.SetIsSfxOn(false);

            // Assert
            Assert.IsFalse(_model.SavedSettingsData.IsSfxOn);
        }

        [Test]
        public void SetIsBgmOn_ShouldUpdateProperty()
        {
            // Act
            _model.SetIsBgmOn(true);

            // Assert
            Assert.IsTrue(_model.SavedSettingsData.IsBgmOn);
        }

        [Test]
        public void SetIsVibrationOn_ShouldUpdateProperty()
        {
            // Act
            _model.SetIsVibrationOn(true);

            // Assert
            Assert.IsTrue(_model.SavedSettingsData.IsVibrationOn);
        }

        [Test]
        public void Initialize_ShouldLoadSavedSettingsData()
        {
            // Arrange
            var savedData = new SavedSettingsData { IsBgmOn = true, IsSfxOn = false, IsVibrationOn = true, IsGameInductionActive = false };
            _mockSaveSystem.Setup(x => x.Load()).Returns(savedData);

            // Act
            var initialize = _controller.Initialize();
            while (initialize.MoveNext()) { }

            // Assert
            Assert.AreEqual(savedData, _model.SavedSettingsData);
        }

        [Test]
        public void DeleteSaveData_ShouldCallDeleteOnSaveSystem()
        {
            // Arrange
            _mockSaveSystem.Setup(x => x.Delete());

            // Act
            _controller.DeleteSaveData(new DeleteSaveDataMessage());

            // Assert
            _mockSaveSystem.Verify(x => x.Delete(), Times.Once);
        }

        [Test]
        public void ToggleGameInduction_ShouldUpdateModelAndSaveData()
        {
            // Arrange
            var message = new ToggleGameInductionMessage(true);

            // Act
            _controller.ToggleGameInduction(message);

            // Assert
            Assert.IsTrue(_model.SavedSettingsData.IsGameInductionActive);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedSettingsData>(y => y.IsGameInductionActive == true)), Times.Once);
        }

        [Test]
        public void ToggleSfx_ShouldUpdateModelAndSaveData()
        {
            // Arrange
            var message = new ToggleSfxMessage(false);

            // Act
            _controller.ToggleSfx(message);

            // Assert
            Assert.IsFalse(_model.SavedSettingsData.IsSfxOn);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedSettingsData>(y => y.IsSfxOn == false)), Times.Once);
        }

        [Test]
        public void ToggleBgm_ShouldUpdateModelAndSaveData()
        {
            // Arrange
            var message = new ToggleBgmMessage(true);

            // Act
            _controller.ToggleBgm(message);

            // Assert
            Assert.IsTrue(_model.SavedSettingsData.IsBgmOn);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedSettingsData>(y => y.IsBgmOn == true)), Times.Once);
        }

        [Test]
        public void ToggleVibration_ShouldUpdateModelAndSaveData()
        {
            // Arrange
            var message = new ToggleVibrationMessage(true);

            // Act
            _controller.ToggleVibration(message);

            // Assert
            Assert.IsTrue(_model.SavedSettingsData.IsVibrationOn);
            _mockSaveSystem.Verify(x => x.Save(It.Is<SavedSettingsData>(y => y.IsVibrationOn == true)), Times.Once);
        }
    }
}
