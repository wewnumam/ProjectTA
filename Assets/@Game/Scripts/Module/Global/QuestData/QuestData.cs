using System;
using UnityEngine;

namespace ProjectTA.Module.QuestData
{
    [Serializable]
    public class QuestData
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
        public int CurrentLevelPlayedAmount { get; set; } = 0;

        [field: SerializeField]
        public int CurrentGameWinAmount { get; set; } = 0;

        [field: SerializeField]
        public float CurrentMinutesPlayedAmount { get; set; } = 0;

        [field: SerializeField]
        public float CurrentQuizScoreAmount { get; set; } = 0;
    }
}