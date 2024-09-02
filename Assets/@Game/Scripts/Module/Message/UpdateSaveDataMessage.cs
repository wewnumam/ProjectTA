using ProjectTA.Utility;

namespace ProjectTA.Message
{
    public struct UpdateSaveDataMessage
    {
        public SaveData SaveData { get; }

        public UpdateSaveDataMessage(SaveData saveData) 
        { 
            SaveData = saveData;
        }
    }
}