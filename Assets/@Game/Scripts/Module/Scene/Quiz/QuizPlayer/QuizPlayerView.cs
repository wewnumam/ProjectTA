using Agate.MVC.Base;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.QuizPlayer
{
    public class QuizPlayerView : ObjectView<IQuizPlayerModel>
    {
        [SerializeField] private TMP_Text _questionText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Transform _answerButtonParent;
        [SerializeField] private Button _answerButtonTemplate;

        [SerializeField] private Color _initialColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _wrongColor;

        [SerializeField, ResizableTextArea, ReadOnly] private string _log;

        [field: SerializeField]
        public List<QuizItem> QuizItems { get; set; }

        private void OnValidate()
        {
            if (_answerButtonTemplate.GetComponentInChildren<TMP_Text>() == null)
            {
                Debug.LogError("TMP text not found in answer button template child!");
            }
        }

        private void OnCorrect()
        {
            ButtonsFeedback(_correctColor);
        }

        private void OnWrong()
        {
            ButtonsFeedback(_wrongColor);
        }

        private void ButtonsFeedback(Color color)
        {
            foreach (var button in _model.Buttons)
            {
                button.image.DOColor(color, .5f).OnComplete(() => button.image.DOColor(_initialColor, 1f));
            }
        }

        protected override void InitRenderModel(IQuizPlayerModel model)
        {
            _log = model.GetLog();

            model.InitButtons(_answerButtonTemplate, _answerButtonParent);
            model.AddCallbacks(OnCorrect, OnWrong, null);
        }

        protected override void UpdateRenderModel(IQuizPlayerModel model)
        {
            _log = model.GetLog();

            _questionText.SetText(model.CurrentQuizItem.Question);
            float percentage = (float)(model.AnswersCount - model.WrongCount) / model.AnswersCount;
            _scoreText?.SetText($"{percentage * 100f:F2}%");

            for (int i = 0; i < model.CurrentQuizItem.Answers.Count; i++)
            {
                if (model.ButtonsText[i] == null)
                {
                    model.InitButtons(_answerButtonTemplate, _answerButtonParent);
                }
                else
                {
                    model.ButtonsText[i].SetText(model.CurrentQuizItem.Answers[i].Message);
                }
            }
        }
    }
}