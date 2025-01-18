using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.QuestData
{
    [Serializable]
    public class SavedQuestData
    {
        [field: SerializeField]
        public int CurrentKillAmount { get; set; } = 0;

        [field: SerializeField]
        public int CurrentCollectibleAmount { get; set; } = 0;

        [field: SerializeField]
        public int CurrentPuzzleAmount { get; set; } = 0;

        [field: SerializeField]
        public int CurrentHiddenObjectAmount { get; set; } = 0;

        [field: SerializeField]
        public int CurrentGameWinAmount { get; set; } = 0;

        [field: SerializeField]
        public float CurrentMinutesPlayedAmount { get; set; } = 0;

        [field: SerializeField]
        public float CurrentQuizScoreAmount { get; set; } = 0;

        [field: SerializeField]
        public List<string> LevelPlayed { get; set; } = new List<string>();
    }
}