using ProjectTA.Module.Analytic;

namespace ProjectTA.Message
{
    public struct AnalyticRecordMessage
    {
        public AnalyticRecord AnalyticRecord;

        public AnalyticRecordMessage(AnalyticRecord analyticRecord)
        {
            AnalyticRecord = analyticRecord;
        }
    }
}