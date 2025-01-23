using Moq;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.Analytic;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.GoogleFormUploader;
using ProjectTA.Module.QuizPlayer;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class GoogleFormUploaderTests
    {
        private Mock<IResourceLoader> _mockResourceLoader;
        private GoogleFormUploaderModel _model;
        private GoogleFormUploaderController _controller;
        private GoogleFormUploaderView _view;

        [SetUp]
        public void SetUp()
        {
            _mockResourceLoader = new Mock<IResourceLoader>();
            _model = new GoogleFormUploaderModel();
            _controller = new GoogleFormUploaderController();
            _view = _controller.GetNewGoogleFormUploaderView();

            // Inject dependencies
            _controller.SetModel(_model);
            _controller.SetResourceLoader(_mockResourceLoader.Object);
            _controller.SetView(_view);
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
        public void SetQuizFormConstants_ShouldUpdateConstants()
        {
            // Arrange
            var constants = new QuizFormConstants();

            // Act
            _model.SetQuizFormConstants(constants);

            // Assert
            Assert.AreEqual(constants, _model.QuizFormConstants);
        }

        [UnityTest]
        public IEnumerator Initialize_ShouldSetAnalyticFormConstants()
        {
            // Arrange
            SOGameConstants gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();
            gameConstants.AnalyticFormConstants = new AnalyticFormConstants();
            gameConstants.QuizFormConstants = new QuizFormConstants();

            _mockResourceLoader
                .Setup(x => x.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS))
                .Returns(gameConstants);

            // Act
            yield return _controller.Initialize();

            // Assert
            Assert.AreEqual(gameConstants.AnalyticFormConstants, _model.AnalyticFormConstants);
            Assert.AreEqual(gameConstants.QuizFormConstants, _model.QuizFormConstants);
        }

        [UnityTest]
        public IEnumerator OnSendChoicesRecords_ShouldSendChoicesRecords()
        {
            // Arrage
            SOGameConstants gameConstants = Resources.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS);
            _model.SetQuizFormConstants(gameConstants.QuizFormConstants);

            var choicesRecord = new List<ChoicesRecord> {
                new ChoicesRecord("unit_test_session_id", "unit_test_device_id", "unit_test_question", "unit_test_choices", "unit_test_is_first_choices"),
            };

            var message = new ChoicesRecordsMessage(choicesRecord);

            // Act
            _controller.OnSendChoicesRecords(message);

            // Assert
            LogAssert.Expect(LogType.Log, "Form submitted successfully!");

            yield return new WaitForSeconds(5f);
        }

        [UnityTest]
        public IEnumerator OnSendAnalyticRecord_ShouldSendAnalyticRecord()
        {
            // Arrage
            SOGameConstants gameConstants = Resources.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS);
            _model.SetAnalyticFormConstants(gameConstants.AnalyticFormConstants);

            var analyticRecord = new AnalyticRecord();
            analyticRecord.SessionId = "unit_test";
            analyticRecord.DeviceId = "unit_test";
            analyticRecord.ScreenTimeInSeconds = "unit_test";
            analyticRecord.AverageFps = "unit_test";
            analyticRecord.MaxFps = "unit_test";
            analyticRecord.MinFps = "unit_test";
            analyticRecord.AverageMemory = "unit_test";
            analyticRecord.MaxMemory = "unit_test";
            analyticRecord.MinMemory = "unit_test";
            analyticRecord.LogWarningCount = "unit_test";
            analyticRecord.LogErrorCount = "unit_test";
            analyticRecord.LogExceptionCount = "unit_test";

            var message = new AnalyticRecordMessage(analyticRecord);

            // Act
            _controller.OnSendAnalyticRecord(message);


            // Assert
            LogAssert.Expect(LogType.Log, "Form submitted successfully!");


            yield return new WaitForSeconds(5f);
        }
    }
}
