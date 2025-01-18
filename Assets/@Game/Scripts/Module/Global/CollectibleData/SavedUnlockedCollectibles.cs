using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    [Serializable]
    public class SavedUnlockedCollectibles
    {
        [field: SerializeField]
        public List<string> Items { get; set; } = new List<string>();
    }
}