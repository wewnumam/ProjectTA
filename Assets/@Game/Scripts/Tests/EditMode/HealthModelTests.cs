using NUnit.Framework;
using ProjectTA.Module.Health;
using Unity.PerformanceTesting;

namespace ProjectTA.Tests
{
    public class HealthModelTests
    {
        private HealthModel _healthModel;

        [SetUp]
        public void Setup()
        {
            _healthModel = new HealthModel();
        }

        [Test]
        public void SetInitialHealth_ShouldSetInitialHealthCorrectly()
        {
            // Arrange
            int initialHealth = 100;

            // Act
            _healthModel.SetInitialHealth(initialHealth);

            // Assert
            Assert.AreEqual(initialHealth, _healthModel.InitialHealth);
        }

        [Test]
        public void SetCurrentHealth_ShouldSetCurrentHealthCorrectly()
        {
            // Arrange
            int currentHealth = 80;

            // Act
            _healthModel.SetCurrentHealth(currentHealth);

            // Assert
            Assert.AreEqual(currentHealth, _healthModel.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldIncreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _healthModel.SetInitialHealth(100);
            _healthModel.SetCurrentHealth(50);
            int amountToAdd = 20;

            // Act
            _healthModel.AddCurrentHealth(amountToAdd);

            // Assert
            Assert.AreEqual(70, _healthModel.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldNotExceedInitialHealth()
        {
            // Arrange
            _healthModel.SetInitialHealth(100);
            _healthModel.SetCurrentHealth(90);
            int amountToAdd = 20;

            // Act
            _healthModel.AddCurrentHealth(amountToAdd);

            // Assert
            Assert.AreEqual(100, _healthModel.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldDecreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _healthModel.SetInitialHealth(100);
            _healthModel.SetCurrentHealth(50);
            int amountToSubtract = 20;

            // Act
            _healthModel.SubtractCurrentHealth(amountToSubtract);

            // Assert
            Assert.AreEqual(30, _healthModel.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldNotGoBelowZero()
        {
            // Arrange
            _healthModel.SetInitialHealth(100);
            _healthModel.SetCurrentHealth(10);
            int amountToSubtract = 20;

            // Act
            _healthModel.SubtractCurrentHealth(amountToSubtract);

            // Assert
            Assert.AreEqual(0, _healthModel.CurrentHealth);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsZero()
        {
            // Arrange
            _healthModel.SetCurrentHealth(0);

            // Act
            bool result = _healthModel.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsNegative()
        {
            // Arrange
            _healthModel.SetCurrentHealth(-10);

            // Act
            bool result = _healthModel.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnFalse_WhenCurrentHealthIsPositive()
        {
            // Arrange
            _healthModel.SetCurrentHealth(10);

            // Act
            bool result = _healthModel.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsFalse(result);
        }
    }
}