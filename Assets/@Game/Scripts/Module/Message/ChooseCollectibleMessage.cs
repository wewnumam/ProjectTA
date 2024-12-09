using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Message
{
    public struct ChooseCollectibleMessage
    {
        public SO_CollectibleData CollectibleData { get; }

        public ChooseCollectibleMessage(SO_CollectibleData collectibleData) 
        { 
            CollectibleData = collectibleData;
        }
    }
}