using Agate.MVC.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CharacterData
{
    public class CharacterDataModel : BaseModel, ICharacterDataModel
    {
        public SO_CharacterData CurrentCharacterData { get; private set; }
        public SO_CharacterCollection CharacterCollection { get; private set; }

        public GameObject CurrentPrefab { get; private set; }

        public void SetCurrentCharacterData(SO_CharacterData characterData)
        {
            CurrentCharacterData = characterData;
            SetDataAsDirty();
        }

        public void SetCharacterCollection(SO_CharacterCollection characterCollection)
        {
            CharacterCollection = characterCollection;
            SetDataAsDirty();
        }

        public void SetCurrentPrefab(GameObject currentPrefab)
        {
            CurrentPrefab = currentPrefab;
            SetDataAsDirty();
        }
    }
}