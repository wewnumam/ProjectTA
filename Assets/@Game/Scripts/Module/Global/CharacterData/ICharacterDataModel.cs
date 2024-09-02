using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.CharacterData
{
    public interface ICharacterDataModel : IBaseModel
    {
        SO_CharacterData CurrentCharacterData { get; }
        SO_CharacterCollection CharacterCollection { get; }

        GameObject CurrentPrefab { get; }
    }
}