using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.CollectibleItem;
using UnityEngine;

namespace ProjectTA.Module.CollectibleList
{
    public class CollectibleListController : ObjectController<CollectibleListController, CollectibleDataModel, CollectibleListView>
    {
        public void SetModel(CollectibleDataModel model)
        {
            _model = model;
        }

        public void InitModel(ICollectibleDataModel collectibleData)
        {
            if (collectibleData == null)
            {
                Debug.LogError("COLLECTIBLEDATA IS NULL");
                return;
            }
            if (collectibleData.CollectibleCollection == null)
            {
                Debug.LogError("COLLECTIBLECOLLECTION IS NULL");
                return;
            }

            _model.SetCollectibleCollection(collectibleData.CollectibleCollection);

            if (collectibleData.UnlockedCollectibleItems == null)
            {
                Debug.LogError("UNLOCKEDCOLLECTIBLEITEMS IS NULL");
                return;
            }

            _model.SetUnlockedCollectibleItems(collectibleData.UnlockedCollectibleItems);
        }

        public override void SetView(CollectibleListView view)
        {
            base.SetView(view);

            foreach (var collectibleItem in _model.CollectibleCollection.CollectibleItems)
            {
                GameObject obj = GameObject.Instantiate(view.ItemTemplate.gameObject, view.Parent);

                CollectibleItemView collectibleItemView = obj.GetComponent<CollectibleItemView>();
                CollectibleItemController collectibleItemController = new CollectibleItemController();
                InjectDependencies(collectibleItemController);
                collectibleItemController.Init(collectibleItemView, collectibleItem, _model.UnlockedCollectibleItems.Contains(collectibleItem));

                obj.SetActive(true);
            }

            view.CollectionCountText.SetText($"{_model.UnlockedCollectibleItems.Count}/{_model.CollectibleCollection.CollectibleItems.Count}");
        }

        public void OnChooseColletible(ChooseCollectibleMessage message)
        {
            _view.DescriptionText.SetText(message.CollectibleData.Description);
        }
    }
}