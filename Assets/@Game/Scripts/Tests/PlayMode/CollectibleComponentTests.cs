using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.PerformanceTesting;
using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Tests
{
    public class CollectibleComponentTests
    {
        private GameObject _gameObject;
        private CollectibleComponent _collectibleComponent;
        private SOCollectibleData _collectibleData;

        [SetUp]
        public void Setup()
        {
            _gameObject = new GameObject();
            _collectibleComponent = _gameObject.AddComponent<CollectibleComponent>();
            _collectibleData = ScriptableObject.CreateInstance<SOCollectibleData>();
            _collectibleData.name = "TestCollectible";
            _collectibleComponent.Initialize(_collectibleData);
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_gameObject);
            Object.Destroy(_collectibleData);
        }

        [Test]
        public void Initialize_ShouldSetCollectibleData()
        {
            Assert.IsNotNull(_collectibleComponent.CollectibleData);
            Assert.AreEqual("TestCollectible", _collectibleComponent.CollectibleData.name);
        }

        [Test]
        public void Collect_ShouldLogMessage()
        {
            _collectibleComponent.Collect();
            LogAssert.Expect(LogType.Log, "TestCollectible collected!");
        }

        [Test, Performance]
        public void Performance_Collect_ShouldCompleteInUnderThreshold()
        {
            Measure.Method(() =>
            {
                _collectibleComponent.Collect();
                LogAssert.Expect(LogType.Log, "TestCollectible collected!");
            })
            .WarmupCount(10)
            .MeasurementCount(100)
            .Run();
        }
    }
}