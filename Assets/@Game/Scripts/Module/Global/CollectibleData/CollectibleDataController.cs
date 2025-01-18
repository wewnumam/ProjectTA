using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataController : DataController<CollectibleDataController, CollectibleDataModel, ICollectibleDataModel>
    {
        private SaveSystem<SavedUnlockedCollectibles> _savedUnlockedCollectiblesName = null;

        public void SetModel(CollectibleDataModel model)
        {
            _model = model;
        }

        public override IEnumerator Initialize()
        {
            _savedUnlockedCollectiblesName = new SaveSystem<SavedUnlockedCollectibles>(TagManager.FILENAME_SAVEDUNLOCKEDCOLLECTIBLES);
            _model.SetUnlockedCollectiblesName(_savedUnlockedCollectiblesName.Load());

            try
            {
                SOCollectibleCollection collectibleCollection = Resources.Load<SOCollectibleCollection>(@"CollectibleCollection");
                _model.SetCollectibleCollection(collectibleCollection);
            }
            catch (Exception e)
            {
                Debug.LogError("COLLECTIBLE COLLECTION SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            if (_model.UnlockedCollectiblesName.Items.Count <= 0)
            {
                foreach (var unlockedCollectible in _model.UnlockedCollectiblesName.Items)
                {
                    AddUnlockedCollectible(unlockedCollectible);
                }
            }

            yield return base.Initialize();
        }

        public void AddUnlockedCollectible(string collectibleName)
        {
            try
            {
                SOCollectibleData collectibleData = Resources.Load<SOCollectibleData>(@"CollectibleData/" + collectibleName);
                _model.AddUnlockedCollectibleCollection(collectibleData);
            }
            catch (Exception e)
            {
                Debug.LogError($"{collectibleName} SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }
        }

        public void OnUnlockCollectible(UnlockCollectibleMessage message)
        {
            Debug.Log($"UNLOCK COLLECTIBLE: {message.CollectibleData.Title}");
            _model.AddUnlockedCollectibleCollection(message.CollectibleData);

            _savedUnlockedCollectiblesName.Save(_model.UnlockedCollectiblesName);
        }

        public void OnDeleteSaveData(DeleteSaveDataMessage message)
        {
            _savedUnlockedCollectiblesName.Delete();
        }
    }
}
