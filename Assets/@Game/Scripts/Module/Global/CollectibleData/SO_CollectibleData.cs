using NaughtyAttributes;
using UnityEngine;
using ProjectTA.Utility;

namespace ProjectTA.Module.CollectibleData
{
    [CreateAssetMenu(fileName = "CollectibleData_", menuName = "ProjectTA/CollectibleData", order = 5)]
    public class SO_CollectibleData : ScriptableObject
    {
        [Header("Display")]
        public string Title;
        public string Description;
        public EnumManager.CollectibleType Type;

        [Header("Environment"), ShowAssetPreview]
        public GameObject prefab;
        public TextAsset dialogue;
    }
}
