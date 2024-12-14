using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace ProjectTA.Module.QuizPlayer
{
    public class QuizPlayerController : ObjectController<QuizPlayerController, QuizPlayerView>
    {
        private int _currentQuizItemIndex;
        private int _wrongCount;
        private int _answersCount;
        private List<Button> _buttons = new();

        public override void SetView(QuizPlayerView view)
        {
            base.SetView(view);
            view.SetCallback(OnNext);

            _answersCount = view.Items.Sum(item => item.Answers.Count);
            UpdateQuestionAndAnswers();
        }

        private void UpdateQuestionAndAnswers()
        {
            var currentItem = _view.Items[_currentQuizItemIndex];
            _view.questionText.SetText(currentItem.Question);

            // Clear previous buttons
            foreach (var button in _buttons)
            {
                button.gameObject.SetActive(false);
            }

            // Clear the button list to avoid stale references
            _buttons.Clear();

            for (int i = 0; i < currentItem.Answers.Count; i++)
            {
                var answer = currentItem.Answers[i];
                GameObject obj = GameObject.Instantiate(_view.answerButtonTemplate.gameObject, _view.answerButtonParent);
                obj.GetComponentInChildren<TMP_Text>().SetText(answer.Message);

                Button button = obj.GetComponent<Button>();
                _buttons.Add(button);
                int index = i; // Capture the current index for the listener
                button.onClick.AddListener(() => AnswerCheck(index));

                obj.SetActive(true);
            }
            
            ButtonsFeedback(_view.CorrectColor);
        }

        public void AnswerCheck(int answerIndex)
        {
            EventSystem.current.SetSelectedGameObject(null);
            var currentItem = _view.Items[_currentQuizItemIndex];

            // Check if the answerIndex is valid
            if (answerIndex < 0 || answerIndex >= currentItem.Answers.Count)
            {
                Debug.LogError($"Invalid answer index: {answerIndex}. Current item has {currentItem.Answers.Count} answers.");
                return; // Exit if the index is invalid
            }

            if (currentItem.Answers[answerIndex].isCorrectAnswer)
            {
                OnNext();
            }
            else
            {
                ButtonsFeedback(_view.WrongColor);
                _wrongCount++;
                _buttons[answerIndex].gameObject.SetActive(false);
            }

            UpdateScore();
        }

        private void UpdateScore()
        {
            float percentage = (float)(_answersCount - _wrongCount) / _answersCount;
            _view.scoreText?.SetText($"{percentage * 100f:F2}%");
            Debug.Log($"{_wrongCount} {_answersCount} {percentage}");
        }

        private void OnNext()
        {
            if (++_currentQuizItemIndex >= _view.Items.Count)
            {
                SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);
                return;
            }

            UpdateQuestionAndAnswers();
        }

        private void ButtonsFeedback(Color color)
        {
            foreach (var button in _buttons)
            {
                button.image.DOColor(color, .5f).OnComplete(() => button.image.DOColor(_view.InitialColor, 1f));
            }
        }
    }
}