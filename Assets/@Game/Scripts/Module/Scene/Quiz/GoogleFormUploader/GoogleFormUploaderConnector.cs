using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderConnector : BaseConnector
    {
        private readonly GoogleFormUploaderController _googleFormUploader = new();

        protected override void Connect()
        {
            Subscribe<ChoicesRecordsMessage>(_googleFormUploader.OnSendChoicesRecords);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChoicesRecordsMessage>(_googleFormUploader.OnSendChoicesRecords);
        }
    }
}