using NUnit.Framework;
using ProjectTA.Module.QuizPlayer;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class QuizPlayerTests
    {
        private QuizPlayerModel _model;
        private QuizPlayerController _controller;
        private QuizPlayerView _view;
        private List<QuizItem> _testQuizItems;

        [SetUp]
        public void Setup()
        {
            _model = new QuizPlayerModel();
            _controller = new QuizPlayerController();
            _view = _controller.GetNewQuizPlayerView();

            // Create test quiz items
            _testQuizItems = new List<QuizItem>
            {
                CreateTestQuizItem("Question 1", new[] { ("Correct", true), ("Wrong1", false), ("Wrong2", false) }),
                CreateTestQuizItem("Question 2", new[] { ("Wrong1", false), ("Correct", true) })
            };

            _view.QuizItems = _testQuizItems;
            GameObject buttonTemplate = new GameObject();
            buttonTemplate.AddComponent<Button>();
            GameObject buttonText = new GameObject();
            buttonText.AddComponent<TextMeshProUGUI>();
            buttonText.transform.SetParent(buttonTemplate.transform);

            TestUtility.SetPrivateField(_view, "_answerButtonTemplate", buttonTemplate.GetComponent<Button>());
            TestUtility.SetPrivateField(_view, "_answerButtonParent", new GameObject().transform);
            TestUtility.SetPrivateField(_view, "_questionText", new GameObject().AddComponent<TextMeshProUGUI>());
            TestUtility.SetPrivateField(_view, "_scoreText", new GameObject().AddComponent<TextMeshProUGUI>());

            _model.InitQuizItem(_testQuizItems);
            _controller.SetModel(_model);
            _controller.SetView(_view);

        }

        #region Model Tests

        [Test]
        public void InitQuizItem_ShouldInitializeCorrectly()
        {
            // Act
            _model.InitQuizItem(_testQuizItems);

            // Assert
            Assert.AreEqual(_testQuizItems.Count, _model.QuizItems.Count);
            Assert.AreEqual(_testQuizItems[0], _model.CurrentQuizItem);
            Assert.AreEqual(5, _model.AnswersCount); // 3 answers in first question + 2 in second
        }

        [Test]
        public void AnswerCheck_ShouldRecordChoicesAndTriggerCallbacks()
        {
            // Arrange
            _model.InitQuizItem(_testQuizItems);
            bool correctTriggered = false;
            bool wrongTriggered = false;
            _model.AddCallbacks(() => correctTriggered = true, () => wrongTriggered = true, null);

            // Act - Correct answer
            _model.AnswerCheck(_testQuizItems[0].Answers[0]);

            // Assert
            Assert.AreEqual(1, _model.ChoicesRecords.Count);
            Assert.IsTrue(correctTriggered);
            Assert.IsFalse(wrongTriggered);
            Assert.IsTrue(TestUtility.GetPrivateField<bool>(_model, "_isFirstChoice"));

            // Act - Wrong answer
            _model.AnswerCheck(_testQuizItems[0].Answers[1]);

            // Assert
            Assert.AreEqual(2, _model.ChoicesRecords.Count);
            Assert.IsTrue(wrongTriggered);
        }

        [Test]
        public void SetNextQuizItem_ShouldProgressThroughQuestions()
        {
            // Arrange
            _model.InitQuizItem(_testQuizItems);
            bool doneTriggered = false;
            _model.AddCallbacks(null, null, () => doneTriggered = true);

            // Act & Assert - First next
            _model.SetNextQuizItem();
            Assert.AreEqual(1, _model.CurrentQuizItemIndex);
            Assert.AreEqual(_testQuizItems[1], _model.CurrentQuizItem);
            Assert.IsFalse(doneTriggered);

            // Act & Assert - Final next
            _model.SetNextQuizItem();
            Assert.AreEqual(2, _model.CurrentQuizItemIndex);
            Assert.IsTrue(doneTriggered);
        }

        [Test]
        public void GetScore_ShouldCalculateCorrectPercentage()
        {
            // Arrange
            _model.InitQuizItem(_testQuizItems);
            _model.AddWrongCount();
            _model.AddWrongCount();

            // Act
            float score = _model.GetScore();

            // Assert (5 total answers - 2 wrong = 60% score)
            Assert.AreEqual(60f, score);
        }

        [Test]
        public void GetLog_ShouldContainRelevantInformation()
        {
            // Arrange
            _model.InitQuizItem(_testQuizItems);
            _model.AddWrongCount();

            // Act
            string log = _model.GetLog();

            // Assert
            StringAssert.Contains($"CurrentQuizItemIndex\t: 0", log);
            StringAssert.Contains($"WrongCount\t\t: 1", log);
            StringAssert.Contains($"AnswersCount\t\t: 5", log);
            StringAssert.Contains("Question 1", log);
        }

        #endregion

        #region Controller Tests

        [Test]
        public void SetView_ShouldInitializeModelWithViewData()
        {
            // Act
            _controller.SetView(_view);

            // Assert
            Assert.AreEqual(_testQuizItems.Count, _model.QuizItems.Count);
            Assert.IsNotNull(_model.CurrentQuizItem);
        }

        [UnityTest]
        public IEnumerator OnCorrect_ShouldProgressToNextQuestion()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnCorrect", null);

            // Assert
            Assert.AreEqual(1, _model.CurrentQuizItemIndex);
            yield return null;
        }

        [Test]
        public void OnWrong_ShouldIncrementWrongCount()
        {
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnWrong", null);

            // Assert
            Assert.AreEqual(1, _model.WrongCount);
        }

        [Test]
        public void OnDone_ShouldGetChoicesRecordsAndGetScore()
        {
            // Arrange
            _model.AnswerCheck(_testQuizItems[0].Answers[0]);
            _model.AnswerCheck(_testQuizItems[1].Answers[1]);

            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnDone", null);

            // Assert
            Assert.AreEqual(100f, _model.GetScore());
            Assert.AreEqual(2, _model.ChoicesRecords.Count);
        }

        #endregion

        #region Helper Methods

        private QuizItem CreateTestQuizItem(string question, IEnumerable<(string message, bool isCorrect)> answers)
        {
            var quizItem = new QuizItem();
            quizItem.Question = question;
            quizItem.Answers = new List<QuizAnswer>();

            foreach (var answer in answers)
            {
                quizItem.Answers.Add(new QuizAnswer
                {
                    Message = answer.message,
                    IsCorrectAnswer = answer.isCorrect
                });
            }

            return quizItem;
        }

        #endregion
    }
}