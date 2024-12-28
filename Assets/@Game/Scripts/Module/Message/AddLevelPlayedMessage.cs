namespace ProjectTA.Message
{
    public struct AddLevelPlayedMessage
    {
        public string LevelName { get; }

        public AddLevelPlayedMessage(string levelName) 
        { 
            LevelName = levelName;
        }
    }
}