using Agate.MVC.Base;
using ProjectTA.Module.CollectibleData;
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
                obj.GetComponent<Button>().interactable = true;
                if (obj.TryGetComponent<TMP_Text>(out var text))
                {
                    text.SetText(collectibleItem.Title);
                }
                obj.SetActive(true);
            }
        }
    }
}