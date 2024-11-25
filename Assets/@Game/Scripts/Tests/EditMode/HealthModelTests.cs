using NUnit.Framework;
using ProjectTA.Module.Health;

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
            int initialHealth = 100;

            _healthModel.SetInitialHealth(initialHealth);

            Assert.AreEqual(initialHealth, _healthModel.InitialHealth);
        }

        [Test]
        public void SetCurrentHealth_ShouldSetCurrentHealthCorrectly()
        {
            int currentHealth = 80;

            _healthModel.SetCurrentHealth(currentHealth);

            Assert.AreEqual(currentHealth, _healthModel.CurrentHealth);
        }

        [Test]
        public void AddCurrentHealth_ShouldIncreaseCurrentHealthByGivenAmount()
        {
            _healthModel.SetCurrentHealth(50);
            int amountToAdd = 20;

            _healthModel.AddCurrentHealth(amountToAdd);

            Assert.AreEqual(70, _healthModel.CurrentHealth);
        }

        [Test]
        public void SubtractCurrentHealth_ShouldDecreaseCurrentHealthByGivenAmount()
        {
            _healthModel.SetCurrentHealth(50);
            int amountToSubtract = 20;

            _healthModel.SubtractCurrentHealth(amountToSubtract);

            Assert.AreEqual(30, _healthModel.CurrentHealth);
        }

        [Test]
        public void IsCurrentHealthLessThanZero_ShouldReturnTrue_WhenCurrentHealthIsNegative()
        {
            _healthModel.SetCurrentHealth(-10);

            bool result = _healthModel.IsCurrentHealthLessThanZero();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsCurrentHealthLessThanZero_ShouldReturnFalse_WhenCurrentHealthIsZeroOrPositive()
        {
            _healthModel.SetCurrentHealth(0);

            Assert.IsFalse(_healthModel.IsCurrentHealthLessThanZero());

            _healthModel.SetCurrentHealth(10);

            Assert.IsFalse(_healthModel.IsCurrentHealthLessThanZero());
        }
    }
}
