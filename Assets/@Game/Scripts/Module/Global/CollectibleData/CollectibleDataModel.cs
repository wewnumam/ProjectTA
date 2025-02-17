using Agate.MVC.Base;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataModel : BaseModel, ICollectibleDataModel
    {
        public SOCollectibleCollection CollectibleCollection { get; private set; }
        public List<SOCollectibleData> UnlockedCollectibleItems { get; private set; } = new();
        public SavedUnlockedCollectibles UnlockedCollectiblesName { get; private set; } = new();

        public void SetCollectibleCollection(SOCollectibleCollection collectibleCollection)
        {
            CollectibleCollection = collectibleCollection;
        }

        public void SetUnlockedCollectibleItems(List<SOCollectibleData> unlockedCollectibleItems)
        {
            UnlockedCollectibleItems = unlockedCollectibleItems;
        }

        public void AddUnlockedCollectibleCollection(SOCollectibleData collectibleData)
        {
            if (UnlockedCollectibleItems.Contains(collectibleData))
            {
                Debug.Log($"{collectibleData.name} is already unlocked!");
            }
            else
            {
                UnlockedCollectibleItems.Add(collectibleData);
            }

            if (UnlockedCollectiblesName.Items.Contains(collectibleData.name))
            {
                Debug.Log($"{collectibleData.name} is already saved!");
            }
            else
            {
                UnlockedCollectiblesName.Items.Add(collectibleData.name);
            }
        }

        public void SetUnlockedCollectiblesName(SavedUnlockedCollectibles unlockedCollectiblesName)
        {
            UnlockedCollectiblesName = unlockedCollectiblesName;
        }
    }
}