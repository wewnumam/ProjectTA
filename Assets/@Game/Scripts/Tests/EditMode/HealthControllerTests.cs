using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.Health;
using Unity.PerformanceTesting;

namespace ProjectTA.Tests
{
    public class HealthControllerTests
    {
        private HealthController _healthController;

        [SetUp]
        public void Setup()
        {
            _healthController = new HealthController();
        }

        [Test]
        public void SetInitialHealth_ShouldSetInitialHealthCorrectly()
        {
            // Arrange
            int initialHealth = 100;

            // Act
            _healthController.SetInitialHealth(initialHealth);

            // Assert
            Assert.AreEqual(initialHealth, _healthController.Model.InitialHealth);
            Assert.AreEqual(initialHealth, _healthController.Model.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldIncreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _healthController.SetInitialHealth(100);
            _healthController.SetCurrentHealth(50);
            int amountToAdd = 20;

            // Act
            _healthController.OnAddHealth(new AddHealthMessage(amountToAdd));

            // Assert
            Assert.AreEqual(70, _healthController.Model.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldNotExceedInitialHealth()
        {
            // Arrange
            _healthController.SetInitialHealth(100);
            _healthController.SetCurrentHealth(90);
            int amountToAdd = 20;

            // Act
            _healthController.OnAddHealth(new AddHealthMessage(amountToAdd));

            // Assert
            Assert.AreEqual(100, _healthController.Model.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldDecreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _healthController.SetInitialHealth(100);
            _healthController.SetCurrentHealth(50);
            int amountToSubtract = 20;

            // Act
            _healthController.OnSubtractHealth(new SubtractHealthMessage(amountToSubtract));

            // Assert
            Assert.AreEqual(30, _healthController.Model.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldNotGoBelowZero()
        {
            // Arrange
            _healthController.SetInitialHealth(100);
            _healthController.SetCurrentHealth(10);
            int amountToSubtract = 20;

            // Act
            _healthController.OnSubtractHealth(new SubtractHealthMessage(amountToSubtract));

            // Assert
            Assert.AreEqual(0, _healthController.Model.CurrentHealth);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsZero()
        {
            // Arrange
            _healthController.SetCurrentHealth(0);

            // Act
            bool result = _healthController.Model.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsNegative()
        {
            // Arrange
            _healthController.SetCurrentHealth(-10);

            // Act
            bool result = _healthController.Model.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnFalse_WhenCurrentHealthIsPositive()
        {
            // Arrange
            _healthController.SetCurrentHealth(10);

            // Act
            bool result = _healthController.Model.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsFalse(result);
        }

        [Test, Performance]
        public void Performance_AddCurrentHealth()
        {
            // Arrange
            _healthController.SetInitialHealth(120);
            _healthController.SetCurrentHealth(0);
            int amountToAdd = 1;

            Measure.Method(() =>
            {
                // Act
                _healthController.OnAddHealth(new AddHealthMessage(amountToAdd));
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(110, _healthController.Model.CurrentHealth);
        }

        [Test, Performance]
        public void Performance_SubtractCurrentHealth()
        {
            // Arrange
            _healthController.SetInitialHealth(120);
            int amountToSubtract = 1;

            Measure.Method(() =>
            {
                // Act
                _healthController.OnSubtractHealth(new SubtractHealthMessage(amountToSubtract));
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();

            // Assert
            Assert.AreEqual(10, _healthController.Model.CurrentHealth);
        }
    }
}