using UnityEngine.Events;

namespace ProjectTA.Message
{
    public struct ReportMessage
    {
        public string Message { get; }
        public UnityAction<long> Callback { get; }

        public ReportMessage(string message, UnityAction<long> _callback = null)
        {
            Message = message;
            Callback = _callback;
        }
    }
}