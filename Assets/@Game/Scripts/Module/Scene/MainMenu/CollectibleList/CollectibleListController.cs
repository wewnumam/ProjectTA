using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.CollectibleItem;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CollectibleList
{
    public class CollectibleListController : ObjectController<CollectibleListController, CollectibleListView>
    {
        private SOCollectibleCollection _collectibleCollection = null;
        private List<SOCollectibleData> _unlockedCollectibles = new();

        public void SetCollectibleCollection(SOCollectibleCollection collection)
        {
            _collectibleCollection = collection;
            Debug.Log(_collectibleCollection.CollectibleItems.Count);
        }

        public void SetUnlockedCollectibles(List<SOCollectibleData> unlockedCollectibles)
        {
            _unlockedCollectibles = unlockedCollectibles;
            Debug.Log(_unlockedCollectibles.Count);
        }

        public override void SetView(CollectibleListView view)
        {
            base.SetView(view);

            foreach (var collectibleItem in _collectibleCollection.CollectibleItems)
            {
                GameObject obj = GameObject.Instantiate(view.ItemTemplate.gameObject, view.Parent);

                CollectibleItemView collectibleItemView = obj.GetComponent<CollectibleItemView>();
                CollectibleItemController collectibleItemController = new CollectibleItemController();
                InjectDependencies(collectibleItemController);
                collectibleItemController.Init(collectibleItemView, collectibleItem, _unlockedCollectibles.Contains(collectibleItem));

                obj.SetActive(true);
            }
        }

        internal void OnChooseColletible(ChooseCollectibleMessage message)
        {
            _view.DescriptionText.SetText(message.CollectibleData.Description);
        }
    }
}