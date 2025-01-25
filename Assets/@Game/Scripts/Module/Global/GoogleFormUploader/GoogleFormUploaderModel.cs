using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;
using System;
using UnityEngine;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderModel : BaseModel
    {
        public string SessionId { get; private set; } = string.Empty;
        public string DeviceId { get; private set; } = string.Empty;

        public AnalyticFormConstants AnalyticFormConstants { get; private set; } = null;
        public QuizFormConstants QuizFormConstants { get; private set; } = null;
        public ReportFormConstants ReportFormConstants { get; private set; } = null;

        public void InitId()
        {
            SessionId = Guid.NewGuid().ToString();
            DeviceId = SystemInfo.deviceUniqueIdentifier;
        }

        public void SetAnalyticFormConstants(AnalyticFormConstants analyticFormConstants)
        {
            AnalyticFormConstants = analyticFormConstants;
        }

        public void SetQuizFormConstants(QuizFormConstants quizFormConstants)
        {
            QuizFormConstants = quizFormConstants;
        }

        public void SetReportFormConstants(ReportFormConstants reportFormConstants)
        {
            ReportFormConstants = reportFormConstants;
        }
    }
}