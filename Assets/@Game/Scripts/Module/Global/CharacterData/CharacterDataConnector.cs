using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.CharacterData
{
    public class CharacterDataConnector : BaseConnector
    {
        private CharacterDataController _characterData;

        protected override void Connect()
        {
            Subscribe<ChooseCharacterMessage>(_characterData.OnChooseCharacter);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseCharacterMessage>(_characterData.OnChooseCharacter);
        }
    }
}