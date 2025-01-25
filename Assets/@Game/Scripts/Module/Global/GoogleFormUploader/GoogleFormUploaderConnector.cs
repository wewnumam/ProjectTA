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
            Subscribe<AnalyticRecordMessage>(_googleFormUploader.OnSendAnalyticRecord);
            Subscribe<ReportMessage>(_googleFormUploader.OnSendReport);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChoicesRecordsMessage>(_googleFormUploader.OnSendChoicesRecords);
            Unsubscribe<AnalyticRecordMessage>(_googleFormUploader.OnSendAnalyticRecord);
            Unsubscribe<ReportMessage>(_googleFormUploader.OnSendReport);
        }
    }
}