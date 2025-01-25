namespace ProjectTA.Message
{
    public struct ReportMessage
    {
        public string Message { get; }

        public ReportMessage(string message)
        {
            Message = message;
        }
    }
}