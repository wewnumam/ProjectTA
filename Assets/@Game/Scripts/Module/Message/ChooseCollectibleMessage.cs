using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Message
{
    public struct ChooseCollectibleMessage
    {
        public SOCollectibleData CollectibleData { get; }

        public ChooseCollectibleMessage(SOCollectibleData collectibleData) 
        { 
            CollectibleData = collectibleData;
        }
    }
}