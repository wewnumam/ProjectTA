using System;
using UnityEngine;

namespace ProjectTA.Module.GameConstants
{
    [CreateAssetMenu(fileName = "GameConstants", menuName = "ProjectTA/GameConstants", order = 0)]
    public class SO_GameConstants : ScriptableObject
    {
        public bool isJoystickActive;
        public int initialHealth;

        public ShootingConstants ShootingConstants;
    }

    [Serializable]
    public class ShootingConstants
    {
        [Range(0.0f, 1.0f)]
        public float JoystickShootingRange;
        public int BulletPoolSize;
        public float BulletShootingRate;
        public float BulletForce;
        public float BulletDestroyDelay;
    }
}
