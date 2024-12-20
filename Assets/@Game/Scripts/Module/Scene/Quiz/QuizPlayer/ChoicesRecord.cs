using System;
using UnityEngine;

namespace ProjectTA.Module.QuizPlayer
{
    [Serializable]
    public class ChoicesRecord
    {
        [field: SerializeField]
        public string Question { get; set; } = string.Empty;
        [field: SerializeField]
        public string Choices { get; set; } = string.Empty;
        
        public ChoicesRecord(string question, string choices)
        {
            Question = question;
            Choices = choices;
        }

    }
}