using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataController : DataController<CollectibleDataController, CollectibleDataModel, ICollectibleDataModel>
    {
        #region UTILITY

        private ISaveSystem<SavedUnlockedCollectibles> _savedUnlockedCollectiblesName = null;
        private IResourceLoader _resourceLoader = null;

        public void SetSaveSystem(ISaveSystem<SavedUnlockedCollectibles> savedUnlockedCollectibles)
        {
            _savedUnlockedCollectiblesName = savedUnlockedCollectibles;
        }

        public void SetResourceLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public void SetModel(CollectibleDataModel model)
        {
            _model = model;
        }

        #endregion

        public override IEnumerator Initialize()
        {
            if (_resourceLoader == null)
            {
                _resourceLoader = new ResourceLoader();
            }

            if (_savedUnlockedCollectiblesName == null)
            {
                _savedUnlockedCollectiblesName = new SaveSystem<SavedUnlockedCollectibles>(TagManager.FILENAME_SAVEDUNLOCKEDCOLLECTIBLES);
            }
            _model.SetUnlockedCollectiblesName(_savedUnlockedCollectiblesName.Load());

            LoadCollectibleCollection();

            InitUnlockedCollectible();

            yield return base.Initialize();
        }

        #region PRIVATE METHOD

        private void LoadCollectibleCollection()
        {
            var collectibleCollection = _resourceLoader.Load<SOCollectibleCollection>(TagManager.SO_COLLECTIBLECOLLECTION);
            if (collectibleCollection != null)
            {
                _model.SetCollectibleCollection(collectibleCollection);
            }
            else
            {
                Debug.LogError("COLLECTIBLE COLLECTION SCRIPTABLE NOT FOUND!");
            }
        }

        private void InitUnlockedCollectible()
        {
            if (_model.UnlockedCollectiblesName == null ||
                _model.UnlockedCollectiblesName.Items == null ||
                _model.UnlockedCollectiblesName.Items.Count <= 0)
            {
                return;
            }

            // Create a copy of the list to avoid modification during iteration
            var unlockedCollectiblesCopy = new List<string>(_model.UnlockedCollectiblesName.Items);

            foreach (var unlockedCollectible in unlockedCollectiblesCopy)
            {
                AddUnlockedCollectible(unlockedCollectible);
            }
        }

        private void AddUnlockedCollectible(string collectibleName)
        {
            var collectibleData = _resourceLoader.Load<SOCollectibleData>($"CollectibleData/{collectibleName}");
            if (collectibleData != null)
            {
                if (_model.UnlockedCollectiblesName.Items.Contains(collectibleName))
                {
                    Debug.Log($"{collectibleData.name} is already unlocked!");
                }
                {
                    _model.AddUnlockedCollectibleCollection(collectibleData);
                }
            }
            else
            {
                Debug.LogError($"{collectibleName} SCRIPTABLE NOT FOUND!");
            }
        }

        #endregion

        #region MESSAGE LISTENER

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

        #endregion
    }
}
