using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleComponent : MonoBehaviour
    {
        [field: SerializeField]
        public SOCollectibleData CollectibleData {  get; private set; }

        public void SetCollectibleData(SOCollectibleData collectibleData)
        {
            CollectibleData = collectibleData;
        }
    }
}
