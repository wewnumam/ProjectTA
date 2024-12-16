using Agate.MVC.Base;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataModel : BaseModel, ICollectibleDataModel
    {
        public SOCollectibleCollection CollectibleCollection { get; private set; } = new();
        public List<SOCollectibleData> UnlockedCollectibleItems { get; private set; } = new();

        public void SetCollectibleCollection(SOCollectibleCollection collectibleCollection)
        {
            CollectibleCollection = collectibleCollection;
            SetDataAsDirty();
        }

        public void AddUnlockedCollectibleCollection(SOCollectibleData collectibleData)
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