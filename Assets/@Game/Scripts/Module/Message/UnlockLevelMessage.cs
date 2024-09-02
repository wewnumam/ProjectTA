using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct UnlockLevelMessage
    {
        public SO_LevelData LevelItem { get; }

        public UnlockLevelMessage(SO_LevelData levelItem)
        {
            LevelItem = levelItem;
        }
    }
}