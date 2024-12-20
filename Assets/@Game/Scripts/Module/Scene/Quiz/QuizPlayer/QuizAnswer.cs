using System;
using UnityEngine;

namespace ProjectTA.Module.QuizPlayer
{
    [Serializable]
    public class QuizAnswer
    {
        [field: SerializeField]
        public string Message { get; set; }
        [field: SerializeField]
        public bool IsCorrectAnswer { get; set; }
    }
}