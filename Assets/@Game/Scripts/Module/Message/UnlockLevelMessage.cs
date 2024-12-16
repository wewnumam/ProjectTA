using ProjectTA.Module.LevelData;
using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct UnlockLevelMessage
    {
        public SOLevelData LevelData { get; }

        public UnlockLevelMessage(SOLevelData levelData)
        {
            LevelData = levelData;
        }
    }
}