using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct ChooseLevelMessage
    {
        public string LevelName { get; }

        public ChooseLevelMessage(string levelName) 
        { 
            LevelName = levelName;
        }
    }
}