using NaughtyAttributes;
using ProjectTA.Module.CollectibleData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "ProjectTA/LevelData", order = 1)]
    public class SOLevelData : ScriptableObject
    {
        [Header("Display")]
        [SerializeField] private string _title;
        [SerializeField, TextArea] private string _description;
        [SerializeField, ShowAssetPreview] private Sprite _icon;

        [Header("Puzzle")]
        [SerializeField, TextArea] private string _puzzleQuestion;
        [SerializeField] private List<PuzzleObject> _puzzleObjects;

        [Header("Environment")]
        [SerializeField, ShowAssetPreview] private GameObject _enemyPrefab;
        [SerializeField, ShowAssetPreview] private GameObject _environmentPrefab;
        [SerializeField] private float _countdown;

        [Header("Types")]
        [SerializeField] private bool _isLockedLevel;
        [SerializeField] private SOLevelData _nextLevel;

        [Header("Cutscene")]
        [SerializeField] private SOCutsceneData _cutsceneData;

        [Header("Hidden Object")]
        [SerializeField] private List<SOCollectibleData> _hiddenObjects;

        public string Title => _title;
        public string Description => _description;
        public Sprite Icon => _icon;
        public string PuzzleQuestion => _puzzleQuestion;
        public List<PuzzleObject> PuzzleObjects { get => _puzzleObjects; set => _puzzleObjects = value; }
        public GameObject EnemyPrefab => _enemyPrefab;
        public GameObject EnvironmentPrefab { get => _environmentPrefab; set => _environmentPrefab = value; }
        public float Countdown => _countdown;
        public bool IsLockedLevel => _isLockedLevel;
        public SOLevelData NextLevel { get => _nextLevel; set => _nextLevel = value; }
        public SOCutsceneData CutsceneData { get => _cutsceneData; set => _cutsceneData = value; }
        public List<SOCollectibleData> HiddenObjects { get => _hiddenObjects; set => _hiddenObjects = value; }
    }

    [Serializable]
    public class PuzzleObject
    {
        [SerializeField] private SOCollectibleData _collectibleData;
        [SerializeField] private Vector3 _objectPosition;
        [SerializeField] private Vector3 _rectPosition;

        public SOCollectibleData CollectibleData => _collectibleData;
        public Vector3 ObjectPosition => _objectPosition;
        public Vector3 RectPosition => _rectPosition;
    }
}
