using NaughtyAttributes;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    [CreateAssetMenu(fileName = "CollectibleData_", menuName = "ProjectTA/CollectibleData", order = 5)]
    public class SOCollectibleData : ScriptableObject
    {
        [Header("Display")]
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private EnumManager.CollectibleType _type;

        [Header("Environment"), ShowAssetPreview]
        [SerializeField] private GameObject _prefab;
        [SerializeField] private TextAsset _dialogue;

        public string Title => _title;
        public string Description => _description;
        public EnumManager.CollectibleType Type => _type;
        public GameObject Prefab => _prefab;
        public TextAsset Dialogue => _dialogue;
    }
}
