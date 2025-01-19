using ProjectTA.Module.QuizPlayer;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.SaveSystem
{
    [System.Serializable]
    public class SavedSettingsData
    {
        [field: SerializeField]
        public bool IsGameInductionActive { get; set; } = true;
        [field: SerializeField]
        public bool IsSfxOn { get; set; } = true;
        [field: SerializeField]
        public bool IsBgmOn { get; set; } = true;
        [field: SerializeField]
        public bool IsVibrationOn { get; set; } = true;

    }
}