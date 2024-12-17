using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.CollectibleList
{
    public class CollectibleListConnector : BaseConnector
    {
        private readonly CollectibleListController _collectibleList = new();

        protected override void Connect()
        {
            Subscribe<ChooseCollectibleMessage>(_collectibleList.OnChooseColletible);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseCollectibleMessage>(_collectibleList.OnChooseColletible);
        }
    }
}