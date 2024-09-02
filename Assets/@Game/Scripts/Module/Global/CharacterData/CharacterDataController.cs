using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CharacterData
{
    public class CharacterDataController : DataController<CharacterDataController, CharacterDataModel, ICharacterDataModel>
    {
        private SaveSystemController _saveSystemController;

        public override IEnumerator Initialize()
        {
            SO_CharacterCollection characterCollection = Resources.Load<SO_CharacterCollection>(@"CharacterCollection");
            _model.SetCharacterCollection(characterCollection);

            yield return base.Initialize();
        }

        public IEnumerator SetCurrentCharacter(string characterName)
        {
            SO_CharacterData characterData = Resources.Load<SO_CharacterData>(@"CharacterData/" + characterName);
            _model.SetCurrentCharacterData(characterData);

            Publish(new LoadCharacterCompleteMessage(characterName, characterData.fullName, _model.CurrentPrefab));

            yield return null;
        }

        internal void OnChooseCharacter(ChooseCharacterMessage message)
        {
            Debug.Log($"CHOOSE LEVEL: {message.CharacterName}");
            GameMain.Instance.RunCoroutine(SetCurrentCharacter(message.CharacterName));
            _saveSystemController.SetCurrentCharacterName(message.CharacterName);
        }
    }
}
