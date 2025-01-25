using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.BugReport
{
    public class BugReportView : BaseView
    {
        [SerializeField] private TMP_InputField reportInputField;

        private UnityAction<string> _reportMessage;

        public void SetCallback(UnityAction<string> reportMessage)
        {
            _reportMessage = reportMessage;
        }

        public void SendReport()
        {
            _reportMessage?.Invoke(reportInputField.text);
        }
    }
}