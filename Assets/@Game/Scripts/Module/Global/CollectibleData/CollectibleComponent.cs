using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleComponent : MonoBehaviour
    {
        [field: SerializeField]
        public SOCollectibleData CollectibleData { get; private set; }

        public void Initialize(SOCollectibleData collectibleData)
        {
            CollectibleData = collectibleData;
        }

        public void Collect()
        {
            // Logic for collecting the item
            // This could involve notifying a manager, updating a score, etc.
            Debug.Log($"{CollectibleData.name} collected!");
        }
    }
}
