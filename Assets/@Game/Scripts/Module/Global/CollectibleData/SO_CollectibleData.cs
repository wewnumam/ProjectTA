using NaughtyAttributes;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    [CreateAssetMenu(fileName = "CollectibleData_", menuName = "ProjectTA/CollectibleData", order = 5)]
    public class SO_CollectibleData : ScriptableObject
    {
        [Header("Display")]
        public string Title;
        public string Description;

        [Header("Environment"), ShowAssetPreview]
        public GameObject prefab;
        public TextAsset dialogue;
    }
}
