using ProjectTA.Module.CharacterData;

namespace ProjectTA.Message
{
    public struct UnlockCharacterMessage
    {
        public SO_CharacterData CharacterData { get; }

        public UnlockCharacterMessage(SO_CharacterData characterData)
        {
            CharacterData = characterData;
        }
    }
}