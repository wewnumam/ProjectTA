using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.Analytic;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class AnalyticTests
    {
        private Mock<IResourceLoader> _mockResourceLoader;
        private AnalyticModel _model;
        private AnalyticController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockResourceLoader = new Mock<IResourceLoader>();
            _model = new AnalyticModel();
            _controller = new AnalyticController();

            // Inject dependencies
            _controller.SetModel(_model);
            _controller.SetResourceLoader(_mockResourceLoader.Object);
        }

        [Test]
        public void SetScreenTimeInSeconds_ShouldUpdateScreenTime()
        {
            // Act
            _model.SetScreenTimeInSeconds(123.45f);

            // Assert
            Assert.AreEqual(123, _model.ScreenTimeInSeconds);
        }

        [Test]
        public void SetPerformanceMetrics_ShouldUpdateMetrics()
        {
            // Arrange
            var metrics = new PerformanceMetricsData
            {
                AverageFps = 60,
                MaxFps = 120,
                MinFps = 30,
                AverageMemory = 500,
                MaxMemory = 1000,
                MinMemory = 250
            };

            // Act
            _model.SetPerformanceMetrics(metrics);

            // Assert
            Assert.AreEqual(metrics, _model.PerformanceMetricsData);
        }

        [Test]
        public void AddLogCounter_ShouldUpdateLogCounts()
        {
            // Act
            _model.AddLogCounter(LogType.Warning);
            _model.AddLogCounter(LogType.Error);
            _model.AddLogCounter(LogType.Exception);

            // Assert
            Assert.AreEqual(1, _model.LogWarningCount);
            Assert.AreEqual(1, _model.LogErrorCount);
            Assert.AreEqual(1, _model.LogExceptionCount);
        }

        [Test]
        public void SetIsAppQuit_ShouldUpdateAppQuitState()
        {
            // Act
            _model.SetIsAppQuit(true);

            // Assert
            Assert.IsTrue(_model.IsAppQuit);
        }

        [Test]
        public void SetAnalyticFormConstants_ShouldUpdateConstants()
        {
            // Arrange
            var constants = new AnalyticFormConstants { FpsUpdateInterval = 0.5f };

            // Act
            _model.SetAnalyticFormConstants(constants);

            // Assert
            Assert.AreEqual(constants, _model.AnalyticFormConstants);
        }

        [Test]
        public void GetAnalyticRecord_ShouldReturnValidRecord()
        {
            // Arrange
            _model.SetScreenTimeInSeconds(120);
            _model.SetPerformanceMetrics(new PerformanceMetricsData
            {
                AverageFps = 60,
                MaxFps = 120,
                MinFps = 30,
                AverageMemory = 500,
                MaxMemory = 1000,
                MinMemory = 250
            });
            _model.AddLogCounter(LogType.Warning);
            _model.AddLogCounter(LogType.Error);

            // Act
            var record = _model.GetAnalyticRecord();

            // Assert
            Assert.AreEqual("120", record.ScreenTimeInSeconds);
            Assert.AreEqual("60", record.AverageFps);
            Assert.AreEqual("120", record.MaxFps);
            Assert.AreEqual("30", record.MinFps);
            Assert.AreEqual("500", record.AverageMemory);
            Assert.AreEqual("1000", record.MaxMemory);
            Assert.AreEqual("250", record.MinMemory);
            Assert.AreEqual("1", record.LogWarningCount);
            Assert.AreEqual("1", record.LogErrorCount);
        }

        [UnityTest]
        public IEnumerator Initialize_ShouldSetAnalyticFormConstants()
        {
            // Arrange
            var fpsUpdateInterval = 4f;
            SOGameConstants gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();
            gameConstants.AnalyticFormConstants = new AnalyticFormConstants();
            gameConstants.AnalyticFormConstants.FpsUpdateInterval = fpsUpdateInterval;

            _mockResourceLoader
                .Setup(x => x.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS))
                .Returns(gameConstants);

            AnalyticView view = _controller.GetNewAnalyticView();
            _controller.SetView(view);

            // Act
            yield return _controller.Initialize();

            // Assert
            Assert.AreEqual(gameConstants.AnalyticFormConstants, _model.AnalyticFormConstants);
            Assert.AreEqual(fpsUpdateInterval, view.FpsUpdateInterval);
        }

        [Test]
        public void OnUpdatePerformanceMetrics_ShouldUpdateModel()
        {
            // Arrange
            var metrics = new PerformanceMetricsData
            {
                AverageFps = 60,
                MaxFps = 120,
                MinFps = 30,
                AverageMemory = 500,
                MaxMemory = 1000,
                MinMemory = 250
            };

            // Act
            _controller.OnUpdatePerformanceMetrics(metrics);

            // Assert
            Assert.AreEqual(metrics, _model.PerformanceMetricsData);
        }

        [Test]
        public void HandleLog_ShouldUpdateLogCounters()
        {
            // Act
            _controller.HandleLog("Test Warning", "", LogType.Warning);
            _controller.HandleLog("Test Error", "", LogType.Error);
            _controller.HandleLog("Test Exception", "", LogType.Exception);

            // Assert
            Assert.AreEqual(1, _model.LogWarningCount);
            Assert.AreEqual(1, _model.LogErrorCount);
            Assert.AreEqual(1, _model.LogExceptionCount);
        }

        [Test]
        public void OnAppQuit_ShouldPublishAnalyticRecordMessage()
        {
            // Act
            _controller.OnAppQuit(new AppQuitMessage());

            // Assert
            Assert.IsTrue(_model.IsAppQuit);
            Assert.AreEqual((int)Time.unscaledTime, _model.ScreenTimeInSeconds);
        }
    }
}
