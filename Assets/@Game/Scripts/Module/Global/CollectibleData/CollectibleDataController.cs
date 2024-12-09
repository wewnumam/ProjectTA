using Agate.MVC.Base;
using ProjectTA.Message;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataController : DataController<CollectibleDataController, CollectibleDataModel, ICollectibleDataModel>
    {
        public void AddUnlockedCollectible(string collectibleName)
        {
            try
            {
                SO_CollectibleData collectibleData = Resources.Load<SO_CollectibleData>(@"CollectibleData/" + collectibleName);
                _model.AddUnlockedCollectibleCollection(collectibleData);
            }
            catch (Exception e)
            {
                Debug.LogError($"{collectibleName} SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }
        }

        public override IEnumerator Initialize()
        {
            try
            {
                SO_CollectibleCollection collectibleCollection = Resources.Load<SO_CollectibleCollection>(@"CollectibleCollection");
                _model.SetCollectibleCollection(collectibleCollection);
            }
            catch (Exception e) 
            {
                Debug.LogError("COLLECTIBLE COLLECTION SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            yield return base.Initialize();
        }

        internal void OnUnlockCollectible(UnlockCollectibleMessage message)
        {
            Debug.Log($"UNLOCK COLLECTIBLE: {message.CollectibleData.Title}");
            _model.AddUnlockedCollectibleCollection(message.CollectibleData);
        }
    }
}
