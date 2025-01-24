using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.Health;

namespace ProjectTA.Tests
{
    public class HealthTests
    {
        private HealthModel _model;
        private HealthController _controller;

        [SetUp]
        public void Setup()
        {
            _model = new HealthModel();
            _controller = new HealthController();
            _controller.SetModel(_model);
        }

        [Test]
        public void SetInitialHealth_ShouldSetInitialHealthCorrectly()
        {
            // Arrange
            int initialHealth = 100;

            // Act
            _model.SetInitialHealth(initialHealth);

            // Assert
            Assert.AreEqual(initialHealth, _model.InitialHealth);
        }

        [Test]
        public void SetCurrentHealth_ShouldSetCurrentHealthCorrectly()
        {
            // Arrange
            int currentHealth = 80;

            // Act
            _model.SetCurrentHealth(currentHealth);

            // Assert
            Assert.AreEqual(currentHealth, _model.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldIncreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _model.SetInitialHealth(100);
            _model.SetCurrentHealth(50);
            int amountToAdd = 20;

            // Act
            _model.AdjustCurrentHealthCount(amountToAdd);

            // Assert
            Assert.AreEqual(70, _model.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldNotExceedInitialHealth()
        {
            // Arrange
            _model.SetInitialHealth(100);
            _model.SetCurrentHealth(90);
            int amountToAdd = 20;

            // Act
            _model.AdjustCurrentHealthCount(amountToAdd);

            // Assert
            Assert.AreEqual(100, _model.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldDecreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _model.SetInitialHealth(100);
            _model.SetCurrentHealth(50);
            int amountToSubtract = -20;

            // Act
            _model.AdjustCurrentHealthCount(amountToSubtract);

            // Assert
            Assert.AreEqual(30, _model.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldNotGoBelowZero()
        {
            // Arrange
            _model.SetInitialHealth(100);
            _model.SetCurrentHealth(10);
            int amountToSubtract = -20;

            // Act
            _model.AdjustCurrentHealthCount(amountToSubtract);

            // Assert
            Assert.AreEqual(0, _model.CurrentHealth);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsZero()
        {
            // Arrange
            _model.SetCurrentHealth(0);

            // Act
            bool result = _model.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsNegative()
        {
            // Arrange
            _model.SetCurrentHealth(-10);

            // Act
            bool result = _model.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthEqualsOrLessThanZero_ShouldReturnFalse_WhenCurrentHealthIsPositive()
        {
            // Arrange
            _model.SetCurrentHealth(10);

            // Act
            bool result = _model.IsCurrentHealthEqualsOrLessThanZero();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SetInitialHealth_ShouldSetInitialHealthAndCurrentHealthCorrectly()
        {
            // Arrange
            int initialHealth = 100;

            // Act
            _controller.SetInitialHealth(initialHealth);

            // Assert
            Assert.AreEqual(initialHealth, _model.InitialHealth);
            Assert.AreEqual(initialHealth, _model.CurrentHealth);
        }

        [Test]
        public void OnAddCurrentHealth_ShouldIncreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _model.SetInitialHealth(100);
            _model.SetCurrentHealth(50);
            int amountToAdd = 20;

            // Act
            _controller.OnAdjustHealthCount(new AdjustHealthCountMessage(amountToAdd));

            // Assert
            Assert.AreEqual(70, _model.CurrentHealth);
        }

        [Test]
        public void OnSubtractCurrentHealth_ShouldDecreaseCurrentHealthByGivenAmount()
        {
            // Arrange
            _model.SetInitialHealth(100);
            _model.SetCurrentHealth(50);
            int amountToSubtract = -20;

            // Act
            _controller.OnAdjustHealthCount(new AdjustHealthCountMessage(amountToSubtract));

            // Assert
            Assert.AreEqual(30, _model.CurrentHealth);
        }
    }
}