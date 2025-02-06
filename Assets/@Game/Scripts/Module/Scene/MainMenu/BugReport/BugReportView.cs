using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.BugReport
{
    public class BugReportView : BaseView
    {
        [SerializeField] private TMP_InputField _reportInputField;
        [SerializeField] private TMP_Text _responsText;
        [SerializeField] private Image _responseBg;
        [SerializeField] private Color _succeedColor;
        [SerializeField] private Color _failedColor;
        [SerializeField] private UnityEvent _onEnd;

        private UnityAction<string> _reportMessage;

        public void SetCallback(UnityAction<string> reportMessage)
        {
            _reportMessage = reportMessage;
        }

        public void SendReport()
        {
            _reportMessage?.Invoke(_reportInputField.text);
        }

        public void SetResponse(long responseCode)
        {
            if (responseCode == 200)
            {
                _responseBg.color = _succeedColor;
                _responsText?.SetText("BERHASIL DIKIRIM!");
            }
            else
            {
                _responseBg.color = _failedColor;
                _responsText?.SetText($"ERROR: {responseCode}");
            }

            _onEnd?.Invoke();
        }
    }
}