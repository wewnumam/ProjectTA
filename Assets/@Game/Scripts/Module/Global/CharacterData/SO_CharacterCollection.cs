using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CharacterData
{
    [CreateAssetMenu(fileName = "CharacterCollection", menuName = "ProjectTA/CharacterCollection", order = 3)]
    public class SO_CharacterCollection : ScriptableObject
    {
        public List<SO_CharacterData> characterItems;
    }
}
