using Agate.MVC.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataModel : BaseModel, ICollectibleDataModel
    {
        public SO_CollectibleCollection CollectibleCollection { get; private set; } = new();
        public List<SO_CollectibleData> UnlockedCollectibleItems { get; private set; } = new();

        public void SetCollectibleCollection(SO_CollectibleCollection collectibleCollection)
        {
            CollectibleCollection = collectibleCollection;
            SetDataAsDirty();
        }

        public void AddUnlockedCollectibleCollection(SO_CollectibleData collectibleData)
        {
            if (UnlockedCollectibleItems.Contains(collectibleData))
            {
                Debug.LogWarning($"{collectibleData.name} is already unlocked!");
            }
            else
            {
                UnlockedCollectibleItems.Add(collectibleData);
            }


            SetDataAsDirty();
        }

    }
}