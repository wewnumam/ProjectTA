using NaughtyAttributes;
using ProjectTA.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "ProjectTA/LevelData", order = 1)]
    public class SO_LevelData : ScriptableObject
    {
        [Header("Display")]
        public string title;
        public string description;
        [ShowAssetPreview]
        public Sprite icon;
        public List<string> puzzlePieceLabels = new List<string>();

        [Header("Environment")]
        public GameObject environmentPrefab;

        [Header("Types")]
        public bool isLockedLevel;
        [ShowIf(nameof(isLockedLevel))]
        public SO_LevelData levelGate;

        [Header("Cutscene")]
        public SO_CutsceneData cutsceneData;
    }
}
