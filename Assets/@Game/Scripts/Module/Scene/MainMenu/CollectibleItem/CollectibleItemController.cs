using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;

namespace ProjectTA.Module.CollectibleItem
{
    public class CollectibleItemController : ObjectController<CollectibleItemController, CollectibleItemView>
    {
        public void Init(CollectibleItemView view, SO_CollectibleData collectibleData, bool isUnlocked)
        {
            SetView(view);
            view.CollectibleData = collectibleData;
            view.title.SetText(collectibleData.Title);
            view.chooseButton.interactable = isUnlocked;
        }

        public override void SetView(CollectibleItemView view)
        {
            base.SetView(view);
            view.SetCallback(OnChooseCollectible);
        }

        private void OnChooseCollectible()
        {
            Publish(new ChooseCollectibleMessage(_view.CollectibleData));
        }
    }
}