using NUnit.Framework;
using ProjectTA.Module.QuizPlayer;
using System.Collections.Generic;

namespace ProjectTA.Tests
{
    public class QuizPlayerModelTests
    {
        private QuizPlayerModel _quizPlayerModel;

        [SetUp]
        public void SetUp()
        {
            _quizPlayerModel = new QuizPlayerModel();
        }

        [Test]
        public void InitQuizItem_SetsCurrentQuizItemAndAnswersCount()
        {
            var quizItems = new List<QuizItem>
        {
            new QuizItem { Question = "Question 1", Answers = new List<QuizAnswer> { new QuizAnswer { Message = "Answer 1", IsCorrectAnswer = true } } },
            new QuizItem { Question = "Question 2", Answers = new List<QuizAnswer> { new QuizAnswer { Message = "Answer 2", IsCorrectAnswer = false } } }
        };

            _quizPlayerModel.InitQuizItem(quizItems);

            Assert.AreEqual(quizItems[0], _quizPlayerModel.CurrentQuizItem);
            Assert.AreEqual(2, _quizPlayerModel.AnswersCount);
        }

        [Test]
        public void SetNextQuizItem_MovesToNextQuizItem()
        {
            var quizItems = new List<QuizItem>
        {
            new QuizItem { Question = "Question 1", Answers = new List<QuizAnswer> { new QuizAnswer { Message = "Answer 1", IsCorrectAnswer = true } } },
            new QuizItem { Question = "Question 2", Answers = new List<QuizAnswer> { new QuizAnswer { Message = "Answer 2", IsCorrectAnswer = false } } }
        };

            _quizPlayerModel.InitQuizItem(quizItems);
            _quizPlayerModel.SetNextQuizItem();

            Assert.AreEqual(quizItems[1], _quizPlayerModel.CurrentQuizItem);
            Assert.AreEqual(1, _quizPlayerModel.CurrentQuizItemIndex);
        }

        [Test]
        public void AnswerCheck_CorrectAnswer_IncrementsCorrectCount()
        {
            var quizItems = new List<QuizItem>
        {
            new QuizItem { Question = "Question 1", Answers = new List<QuizAnswer> { new QuizAnswer { Message = "Answer 1", IsCorrectAnswer = true } } }
        };

            _quizPlayerModel.InitQuizItem(quizItems);
            bool wasCorrectCalled = false;
            _quizPlayerModel.AddCallbacks(() => wasCorrectCalled = true, null, null);

            _quizPlayerModel.AnswerCheck(quizItems[0].Answers[0]);

            Assert.IsTrue(wasCorrectCalled);
        }

        [Test]
        public void AnswerCheck_WrongAnswer_IncrementsWrongCount()
        {
            var quizItems = new List<QuizItem>
        {
            new QuizItem { Question = "Question 1", Answers = new List<QuizAnswer> { new QuizAnswer { Message = "Answer 1", IsCorrectAnswer = false } } }
        };

            _quizPlayerModel.InitQuizItem(quizItems);
            bool wasWrongCalled = false;
            _quizPlayerModel.AddCallbacks(null, () =>
            {
                _quizPlayerModel.AddWrongCount();
                wasWrongCalled = true;
            }, null);

            _quizPlayerModel.AnswerCheck(quizItems[0].Answers[0]);

            Assert.IsTrue(wasWrongCalled);
            Assert.AreEqual(1, _quizPlayerModel.WrongCount);
        }
    }
}