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
        [SerializeField] private List<PuzzleObject> _collectibleObjects;

        [Header("Environment")]
        [SerializeField, ShowAssetPreview] private GameObject _enemyPrefab;
        [SerializeField, ShowAssetPreview] private GameObject _environmentPrefab;
        [SerializeField] private float _countdown;

        [Header("Types")]
        [SerializeField] private bool _isLockedLevel;
        [SerializeField, ShowIf(nameof(_isLockedLevel))] private SOLevelData _levelGate;

        [Header("Cutscene")]
        [SerializeField] private SOCutsceneData _cutsceneData;

        [Header("Hidden Object")]
        [SerializeField] private List<SOCollectibleData> _hiddenObjects;

        public string Title => _title;
        public string Description => _description;
        public Sprite Icon => _icon;
        public string PuzzleQuestion => _puzzleQuestion;
        public List<PuzzleObject> CollectibleObjects => _collectibleObjects;
        public GameObject EnemyPrefab => _enemyPrefab;
        public GameObject EnvironmentPrefab => _environmentPrefab;
        public float Countdown => _countdown;
        public bool IsLockedLevel => _isLockedLevel;
        public SOLevelData LevelGate => _levelGate;
        public SOCutsceneData CutsceneData => _cutsceneData;
        public List<SOCollectibleData> HiddenObjects => _hiddenObjects;
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
