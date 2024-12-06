using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataConnector : BaseConnector
    {
        private CollectibleDataController _collectibleData;

        protected override void Connect()
        {
            Subscribe<UnlockCollectibleMessage>(_collectibleData.OnUnlockCollectible);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UnlockCollectibleMessage>(_collectibleData.OnUnlockCollectible);
        }
    }
}