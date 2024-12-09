using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.CollectibleItem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.CollectibleList
{
    public class CollectibleListController : ObjectController<CollectibleListController, CollectibleListView>
    {
        private SO_CollectibleCollection _collectibleCollection;
        private List<SO_CollectibleData> _unlockedCollectibles;

        public void SetCollectibleCollection(SO_CollectibleCollection collection)
        {
            _collectibleCollection = collection;
        }

        public void SetUnlockedCollectibles(List<SO_CollectibleData> unlockedCollectibles)
        {
            _unlockedCollectibles = unlockedCollectibles;
        }

        public override void SetView(CollectibleListView view)
        {
            base.SetView(view);

            foreach (var collectibleItem in _collectibleCollection.CollectibleItems)
            {
                GameObject obj = GameObject.Instantiate(view.itemTemplate.gameObject, view.parent);

                CollectibleItemView collectibleItemView = obj.GetComponent<CollectibleItemView>();
                _view.collectibleItems.Add(collectibleItemView);

                CollectibleItemController collectibleItemController = new CollectibleItemController();
                InjectDependencies(collectibleItemController);
                collectibleItemController.Init(collectibleItemView, collectibleItem, _unlockedCollectibles.Contains(collectibleItem));

                obj.SetActive(true);
            }
        }

        internal void OnChooseColletible(ChooseCollectibleMessage message)
        {
            _view.descriptionText.SetText(message.CollectibleData.Description);
        }
    }
}