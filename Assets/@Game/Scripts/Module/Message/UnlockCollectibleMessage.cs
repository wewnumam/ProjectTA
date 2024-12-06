using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Message
{
    public struct UnlockCollectibleMessage
    {
        public SO_CollectibleData CollectibleData { get; }

        public UnlockCollectibleMessage(SO_CollectibleData collectibleData) 
        { 
            CollectibleData = collectibleData;
        }
    }
}