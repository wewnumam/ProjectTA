using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.QuizPlayer
{
    [Serializable]
    public class QuizItem
    {
        [field: SerializeField]
        public CinemachineVirtualCamera VirtualCamera { get; set; }
        [field: SerializeField]
        public string Question { get; set; }
        [field: SerializeField]
        public List<QuizAnswer> Answers { get; set; }
    }
}