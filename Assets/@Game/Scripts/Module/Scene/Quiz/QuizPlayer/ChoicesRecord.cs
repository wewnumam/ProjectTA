using System;
using UnityEngine;

namespace ProjectTA.Module.QuizPlayer
{
    [Serializable]
    public class ChoicesRecord
    {
        [field: SerializeField]
        public string SessionId { get; set; }
        [field: SerializeField]
        public string DeviceId { get; set; }
        [field: SerializeField]
        public string Question { get; set; }
        [field: SerializeField]
        public string Choices { get; set; }
        [field: SerializeField]
        public string IsFirstChoice { get; set; }

        public ChoicesRecord(string sessionId, string deviceId, string question, string choices, string isFirstChoice)
        {
            SessionId = sessionId;
            DeviceId = deviceId;
            Question = question;
            Choices = choices;
            IsFirstChoice = isFirstChoice;
        }
    }
}