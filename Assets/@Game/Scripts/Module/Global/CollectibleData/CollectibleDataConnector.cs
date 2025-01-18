using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.CollectibleData
{
    public class CollectibleDataConnector : BaseConnector
    {
        private readonly CollectibleDataController _collectibleData = new();

        protected override void Connect()
        {
            Subscribe<UnlockCollectibleMessage>(_collectibleData.OnUnlockCollectible);
            Subscribe<DeleteSaveDataMessage>(_collectibleData.OnDeleteSaveData);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UnlockCollectibleMessage>(_collectibleData.OnUnlockCollectible);
            Unsubscribe<DeleteSaveDataMessage>(_collectibleData.OnDeleteSaveData);
        }
    }
}