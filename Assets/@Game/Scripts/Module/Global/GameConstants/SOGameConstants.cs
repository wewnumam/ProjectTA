using ProjectTA.Module.Analytic;
using ProjectTA.Module.QuizPlayer;
using System;
using UnityEngine;

namespace ProjectTA.Module.GameConstants
{
    [CreateAssetMenu(fileName = "GameConstants", menuName = "ProjectTA/GameConstants", order = 0)]
    public class SOGameConstants : ScriptableObject
    {
        [Header("General Settings")]
        [SerializeField] private bool _isJoystickActive;
        [SerializeField] private int _initialHealth;

        [Header("Player Constants")]
        [SerializeField] private PlayerConstants _playerConstants;

        [Header("Shooting Constants")]
        [SerializeField] private ShootingConstants _shootingConstants;

        [Header("Enemy Constants")]
        [SerializeField] private EnemyConstants _enemyConstants;

        [Header("Quiz Form Constants")]
        [SerializeField] private QuizFormConstants _quizFormConstants;

        [Header("Analytic Form Constants")]
        [SerializeField] private AnalyticFormConstants _analyticFormConstants;
        
        [Header("Report Form Constants")]
        [SerializeField] private ReportFormConstants _reportFormConstants;


        public bool IsJoystickActive => _isJoystickActive;
        public int InitialHealth => _initialHealth;
        public PlayerConstants PlayerConstants => _playerConstants;
        public ShootingConstants Shooting => _shootingConstants;
        public EnemyConstants Enemy => _enemyConstants;
        public QuizFormConstants QuizFormConstants { get => _quizFormConstants; set => _quizFormConstants = value; }
        public AnalyticFormConstants AnalyticFormConstants { get => _analyticFormConstants; set => _analyticFormConstants = value; }
        public ReportFormConstants ReportFormConstants { get => _reportFormConstants; set => _reportFormConstants = value; }
    }

    [Serializable]
    public class PlayerConstants
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rayDistance;
        [SerializeField] private float _fixedYPosition;
        [SerializeField] private float _knockbackForce;

        public LayerMask EnemyLayer => _enemyLayer;
        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float RayDistance => _rayDistance;
        public float FixedYPosition => _fixedYPosition;
        public float KnockbackForce => _knockbackForce;
    }

    [Serializable]
    public class ShootingConstants
    {
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _joystickShootingRange;
        [SerializeField] private int _bulletPoolSize;
        [SerializeField] private float _bulletShootingRate;
        [SerializeField] private float _bulletForce;
        [SerializeField] private float _bulletDestroyDelay;

        public float JoystickShootingRange => _joystickShootingRange;
        public int BulletPoolSize => _bulletPoolSize;
        public float BulletShootingRate => _bulletShootingRate;
        public float BulletForce => _bulletForce;
        public float BulletDestroyDelay => _bulletDestroyDelay;
    }

    [Serializable]
    public class EnemyConstants
    {
        [SerializeField] private int _enemyPoolSize;
        [SerializeField] private float _enemySpawnDistance;
        [SerializeField] private float _enemySpawnInterval;
        [SerializeField] private float _enemyMassiveSpawnInterval;
        [SerializeField] private float _enemyDestroyDelay;

        public int EnemyPoolSize => _enemyPoolSize;
        public float EnemySpawnDistance => _enemySpawnDistance;
        public float EnemySpawnInterval => _enemySpawnInterval;
        public float EnemyMassiveSpawnInterval => _enemyMassiveSpawnInterval;
        public float EnemyDestroyDelay => _enemyDestroyDelay;
    }

    [Serializable]
    public class QuizFormConstants
    {
        [SerializeField] private string _formUrl;
        [SerializeField] private ChoicesRecord _entryIds;

        public string FormUrl => _formUrl;
        public ChoicesRecord EntryIds => _entryIds;
    }

    [Serializable]
    public class AnalyticFormConstants
    {
        [field: SerializeField] public float FpsUpdateInterval { get; set; }
        [field: SerializeField] public string FormUrl { get; set; }
        [field: SerializeField] public AnalyticRecord EntryIds { get; set; }
    }

    [Serializable]
    public class ReportFormConstants
    {
        [field: SerializeField] public string FormUrl { get; set; }
        [field: SerializeField] public string SessionId { get; set; }
        [field: SerializeField] public string DeviceId { get; set; }
        [field: SerializeField] public string MessageId { get; set; }
    }

}
