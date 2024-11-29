using NaughtyAttributes;
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

        [Header("Environment")]
        public GameObject environmentPrefab;
        public float countdown;

        [Header("Types")]
        public bool isLockedLevel;
        [ShowIf(nameof(isLockedLevel))]
        public SO_LevelData levelGate;

        [Header("Cutscene")]
        public SO_CutsceneData cutsceneData;
    }
}
