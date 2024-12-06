using NaughtyAttributes;
using ProjectTA.Module.CollectibleData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "ProjectTA/LevelData", order = 1)]
    public class SO_LevelData : ScriptableObject
    {
        [Header("Display")]
        public string title;
        [TextArea]
        public string description;
        [ShowAssetPreview]
        public Sprite icon;

        [Header("Puzzle"), TextArea]
        public string puzzleQuestion;

        [Header("Environment")]
        public GameObject environmentPrefab;
        public float countdown;

        [Header("Types")]
        public bool isLockedLevel;
        [ShowIf(nameof(isLockedLevel))]
        public SO_LevelData levelGate;

        [Header("Cutscene")]
        public SO_CutsceneData cutsceneData;

        [Header("Collectible")]
        public List<CollectibleObject> collectibleObjects;
    }

    [Serializable]
    public class CollectibleObject
    {
        public SO_CollectibleData collectibleData;
        public Vector3 objectPosition;
        public Vector3 rectPosition;
    }
}
