using NaughtyAttributes;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "ProjectTA/LevelCollection", order = 2)]
    public class SO_LevelCollection : ScriptableObject
    {
        public List<SO_LevelData> levelItems;
    }
}
