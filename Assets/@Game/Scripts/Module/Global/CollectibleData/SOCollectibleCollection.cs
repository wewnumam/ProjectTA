using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    [CreateAssetMenu(fileName = "CollectibleCollection", menuName = "ProjectTA/CollectibleCollection", order = 6)]
    public class SOCollectibleCollection : ScriptableObject
    {
        [SerializeField] private List<SOCollectibleData> _collectibleItems;

        public List<SOCollectibleData> CollectibleItems => _collectibleItems;
    }
}
