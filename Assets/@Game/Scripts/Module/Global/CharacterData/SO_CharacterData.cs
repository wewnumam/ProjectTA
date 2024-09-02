using NaughtyAttributes;
using UnityEngine;

namespace ProjectTA.Module.CharacterData
{
    [CreateAssetMenu(fileName = "CharacterData_", menuName = "ProjectTA/CharacterData", order = 1)]
    public class SO_CharacterData : ScriptableObject
    {
        [Header("Display")]
        public string fullName;
        public GameObject prefab;

        [Header("Price")]
        public int cost;
        public bool isUnlockByStar;
        [ShowIf(nameof(isUnlockByStar))] public int starAmount;
    }
}
