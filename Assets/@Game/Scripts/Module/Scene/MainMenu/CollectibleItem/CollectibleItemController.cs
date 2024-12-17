using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Module.CollectibleItem
{
    public class CollectibleItemController : ObjectController<CollectibleItemController, CollectibleItemView>
    {
        private SOCollectibleData _collectibleData = null;

        public void Init(CollectibleItemView view, SOCollectibleData collectibleData, bool isUnlocked)
        {
            SetView(view);
            _collectibleData = collectibleData;
            view.Title.SetText(collectibleData.Title);
            view.ChooseButton.interactable = isUnlocked;
        }

        public override void SetView(CollectibleItemView view)
        {
            base.SetView(view);
            view.SetCallback(OnChooseCollectible);
        }

        private void OnChooseCollectible()
        {
            Publish(new ChooseCollectibleMessage(_collectibleData));
        }
    }
}