using Agate.MVC.Base;
using Cinemachine;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.QuizPlayer
{
    public class QuizPlayerView : BaseView
    {
        public TMP_Text questionText;
        public TMP_Text scoreText;
        public Transform answerButtonParent;
        public Button answerButtonTemplate;
        public Color InitialColor;
        public Color CorrectColor;
        public Color WrongColor;
        public List<QuizItem> Items;

        private UnityAction _onNext;

        public void SetCallback(UnityAction onNext)
        {
            _onNext = onNext;
        }

        public void Next()
        {
            _onNext?.Invoke();
        }
    }

    [Serializable]
    public class QuizItem
    {
        public CinemachineVirtualCamera VirtualCamera;
        public string Question;
        public List<QuizAnswer> Answers;
    }

    [Serializable]
    public class QuizAnswer
    {
        public string Message;
        public bool isCorrectAnswer;
    }
}