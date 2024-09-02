using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct ChooseCharacterMessage
    {
        public string CharacterName { get; }

        public ChooseCharacterMessage(string characterName) 
        { 
            CharacterName = characterName;
        }
    }
}