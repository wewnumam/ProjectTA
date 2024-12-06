using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    [CreateAssetMenu(fileName = "CollectibleCollection", menuName = "ProjectTA/CollectibleCollection", order = 6)]
    public class SO_CollectibleCollection : ScriptableObject
    {
        public List<SO_CollectibleData> CollectibleItems;
    }
}
