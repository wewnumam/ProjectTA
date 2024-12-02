using ProjectTA.Module.LevelData;
using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct UnlockLevelMessage
    {
        public SO_LevelData LevelData { get; }

        public UnlockLevelMessage(SO_LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}