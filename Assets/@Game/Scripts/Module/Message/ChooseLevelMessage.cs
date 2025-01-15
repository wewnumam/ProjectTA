using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct ChooseLevelMessage
    {
        public SOLevelData LevelData { get; }

        public ChooseLevelMessage(SOLevelData levelData)
        {
            LevelData = levelData;
        }
    }
}