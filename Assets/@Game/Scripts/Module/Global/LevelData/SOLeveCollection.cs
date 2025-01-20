using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "LevelCollection", menuName = "ProjectTA/LevelCollection", order = 2)]
    public class SOLevelCollection : ScriptableObject
    {

        [SerializeField] private List<SOLevelData> _levelItems;

        public List<SOLevelData> LevelItems
        {
            get { return _levelItems; }
            set { _levelItems = value; }
        }
    }
}
