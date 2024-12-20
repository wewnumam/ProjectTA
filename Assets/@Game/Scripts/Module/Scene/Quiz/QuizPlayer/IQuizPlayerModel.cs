using Agate.MVC.Base;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.QuizPlayer
{
    public interface IQuizPlayerModel : IBaseModel
    {
        List<QuizItem> QuizItems { get; }
        QuizItem CurrentQuizItem {  get; }

        int WrongCount { get; }
        int AnswersCount { get; }
        List<Button> Buttons { get; }
        List<TMP_Text> ButtonsText { get; }

        void InitButtons(Button template, Transform parent);
        void AddCallbacks(UnityAction onCorrect, UnityAction onWrong, UnityAction onDone);
        string GetLog();
    }
}