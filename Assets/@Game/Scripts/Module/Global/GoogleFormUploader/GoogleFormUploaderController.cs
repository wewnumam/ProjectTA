using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderController : ObjectController<GoogleFormUploaderController, GoogleFormUploaderView>
    {
        private QuizFormConstants _quizFormConstants;
        private AnalyticFormConstants _analyticFormConstants;

        public override IEnumerator Initialize()
        {
            GameMain.Instance.gameObject.AddComponent<GoogleFormUploaderView>();
            SetView(GameMain.Instance.gameObject.GetComponent<GoogleFormUploaderView>());

            try
            {
                SOGameConstants gameConstants = Resources.Load<SOGameConstants>(@"GameConstants");
                _quizFormConstants = gameConstants.QuizFormConstants;
                _analyticFormConstants = gameConstants.AnalyticFormConstants;
            }
            catch (Exception e)
            {
                Debug.LogError("GAMECONSTANT SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            yield return base.Initialize();
        }

        public void OnSendChoicesRecords(ChoicesRecordsMessage message)
        {
            foreach (var choicesRecord in message.ChoicesRecords)
            {
                Dictionary<string, string> keyValues = new()
                {
                    { _quizFormConstants.EntryIds.SessionId, choicesRecord.SessionId },
                    { _quizFormConstants.EntryIds.DeviceId, choicesRecord.DeviceId },
                    { _quizFormConstants.EntryIds.Question, choicesRecord.Question },
                    { _quizFormConstants.EntryIds.Choices, choicesRecord.Choices },
                    { _quizFormConstants.EntryIds.IsFirstChoice, choicesRecord.IsFirstChoice }
                };

                _view.SubmitForm(_quizFormConstants.FormUrl, keyValues);
            }
        }

        public void OnSendAnalyticRecord(AnalyticRecordMessage message)
        {
            Dictionary<string, string> keyValues = new()
            {
                    { _analyticFormConstants.EntryIds.SessionId, message.AnalyticRecord.SessionId },
                    { _analyticFormConstants.EntryIds.DeviceId, message.AnalyticRecord.DeviceId },
                    { _analyticFormConstants.EntryIds.ScreenTimeInSeconds, message.AnalyticRecord.ScreenTimeInSeconds },
                    { _analyticFormConstants.EntryIds.AverageFps, message.AnalyticRecord.AverageFps },
                    { _analyticFormConstants.EntryIds.MaxFps, message.AnalyticRecord.MaxFps },
                    { _analyticFormConstants.EntryIds.MinFps, message.AnalyticRecord.MinFps },
                    { _analyticFormConstants.EntryIds.AverageMemory, message.AnalyticRecord.AverageMemory },
                    { _analyticFormConstants.EntryIds.MaxMemory, message.AnalyticRecord.MaxMemory },
                    { _analyticFormConstants.EntryIds.MinMemory, message.AnalyticRecord.MinMemory },
                    { _analyticFormConstants.EntryIds.LogWarningCount, message.AnalyticRecord.LogWarningCount },
                    { _analyticFormConstants.EntryIds.LogErrorCount, message.AnalyticRecord.LogErrorCount },
                    { _analyticFormConstants.EntryIds.LogExceptionCount, message.AnalyticRecord.LogExceptionCount },
                };

            _view.SubmitForm(_analyticFormConstants.FormUrl, keyValues);
        }
    }
}