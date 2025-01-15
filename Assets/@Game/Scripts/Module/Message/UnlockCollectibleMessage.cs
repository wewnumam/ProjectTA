using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Message
{
    public struct UnlockCollectibleMessage
    {
        public SOCollectibleData CollectibleData { get; }

        public UnlockCollectibleMessage(SOCollectibleData collectibleData)
        {
            CollectibleData = collectibleData;
        }
    }
}