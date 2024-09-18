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

        [Header("Environment")]
        public GameObject environmentPrefab;
    }
}
