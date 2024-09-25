using ProjectTA.Module.LevelData;
using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct GameResultMessage
    {
        public SO_LevelData LevelData { get; }

        public GameResultMessage(SO_LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}