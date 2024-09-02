using UnityEngine;

namespace ProjectTA.Message
{
    public struct LoadCharacterCompleteMessage
    {
        public string CharacterName { get; }
        public string CharacterFullName { get; }
        public GameObject CharacterPrefab { get; }
        
        public LoadCharacterCompleteMessage(string characterName, string characterFullName, GameObject prefab)
        {
            CharacterName = characterName;
            CharacterFullName = characterFullName;
            CharacterPrefab = prefab;
        }
    }
}