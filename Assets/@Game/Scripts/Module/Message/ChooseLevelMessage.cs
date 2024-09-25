using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct ChooseLevelMessage
    {
        public SO_LevelData LevelData { get; }

        public ChooseLevelMessage(SO_LevelData levelData) 
        { 
            LevelData = levelData;
        }
    }
}