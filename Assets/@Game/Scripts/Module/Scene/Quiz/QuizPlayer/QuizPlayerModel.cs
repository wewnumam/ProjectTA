using Agate.MVC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.QuizPlayer
{
    public class QuizPlayerModel : BaseModel, IQuizPlayerModel
    {
        public List<QuizItem> QuizItems { get; private set; } = new();
        public QuizItem CurrentQuizItem { get; private set; } = null;
        public int CurrentQuizItemIndex { get; private set; } = 0;
        public int AnswersCount { get; private set; } = 0;
        public int WrongCount { get; private set; } = 0;

        public List<TMP_Text> ButtonsText { get; private set; } = new();
        public List<Button> Buttons { get; private set; } = new();
        public List<ChoicesRecord> ChoicesRecords { get; private set; } = new();

        private string _sessionId = string.Empty;
        private bool _isFirstChoice = true;
        private UnityAction _onWrong = null;
        private UnityAction _onCorrect = null;
        private UnityAction _onDone = null;

        public void InitQuizItem(List<QuizItem> quizItems)
        {
            QuizItems = quizItems;
            CurrentQuizItem = quizItems[0];
            AnswersCount = quizItems.Sum(item => item.Answers.Count);
            _sessionId = Guid.NewGuid().ToString();
        }

        public void InitButtons(Button template, Transform parent)
        {
            for (int i = 0; i < CurrentQuizItem.Answers.Count; i++)
            {
                GameObject obj = GameObject.Instantiate(template.gameObject, parent);
                Button button = obj.TryGetComponent<Button>(out var objButton) ? objButton : obj.AddComponent<Button>();

                if (button != null)
                {
                    Buttons.Add(button);
                    int index = i; // Capture the current index for the listener
                    ResetButton(button, index);
                }

                TMP_Text buttonText = Buttons[i].GetComponentInChildren<TMP_Text>();

                if (buttonText != null)
                {
                    ButtonsText.Add(buttonText);
                    buttonText.SetText(CurrentQuizItem.Answers[i].Message);
                }
                else
                {
                    Debug.LogError("BUTTON HAS NO TMP_TEXT COMPONENT IN CHILDREN");
                }
            }
        }

        private void ResetButton(Button button, int answerIndex)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                button.gameObject.SetActive(false);
                AnswerCheck(CurrentQuizItem.Answers[answerIndex]);
            });
            button.gameObject.SetActive(true);
        }

        public void AnswerCheck(QuizAnswer quizAnswer)
        {
            ChoicesRecords.Add(new ChoicesRecord(_sessionId, SystemInfo.deviceUniqueIdentifier, CurrentQuizItem.Question, quizAnswer.Message, _isFirstChoice.ToString()));
            _isFirstChoice = false;

            if (quizAnswer.IsCorrectAnswer)
            {
                _onCorrect?.Invoke();
            }
            else
            {
                _onWrong?.Invoke();
            }
        }

        public void AddWrongCount()
        {
            WrongCount++;
            SetDataAsDirty();
        }

        public void AddCallbacks(UnityAction onCorrect, UnityAction onWrong, UnityAction onDone)
        {
            _onCorrect += onCorrect;
            _onWrong += onWrong;
            _onDone += onDone;
        }

        public void SetNextQuizItem()
        {
            for (int i = 0; i < QuizItems.Count; i++)
            {
                if (QuizItems[i].VirtualCamera != null)
                {
                    QuizItems[i].VirtualCamera.enabled = i == CurrentQuizItemIndex;
                }
            }

            for (int i = 0; i < Buttons.Count; i++)
            {
                int index = i; // Capture the current index for the listener
                ResetButton(Buttons[index], index);
            }

            CurrentQuizItemIndex++;

            if (CurrentQuizItemIndex >= QuizItems.Count)
            {
                _onDone?.Invoke();
            }
            else
            {
                CurrentQuizItem = QuizItems[CurrentQuizItemIndex];
            }

            _isFirstChoice = true;

            SetDataAsDirty();
        }

        public string GetLog()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("CutscenePlayerModel Log:");
            sb.AppendLine($"{nameof(CurrentQuizItemIndex)}\t: {CurrentQuizItemIndex}");
            sb.AppendLine($"{nameof(WrongCount)}\t\t: {WrongCount}");
            sb.AppendLine($"{nameof(AnswersCount)}\t\t: {AnswersCount}");
            sb.AppendLine($"{nameof(CurrentQuizItem.Question)}\t\t: {CurrentQuizItem.Question}");

            foreach (var item in CurrentQuizItem.Answers)
            {
                sb.AppendLine($"{item.IsCorrectAnswer}\t\t: {item.Message}");
            }

            return sb.ToString();
        }

        public float GetScore()
        {
            float percentage = (float)(AnswersCount - WrongCount) / AnswersCount;
            return (float)Math.Round(percentage * 100f, 2); // Round to 2 decimal places
        }
    }
}