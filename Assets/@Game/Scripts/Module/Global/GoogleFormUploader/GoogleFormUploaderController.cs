using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderController : ObjectController<GoogleFormUploaderController, GoogleFormUploaderModel, GoogleFormUploaderView>
    {
        #region UTILITY

        private IResourceLoader _resourceLoader = null;

        public void SetResourceLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public void SetModel(GoogleFormUploaderModel model)
        {
            _model = model;
        }

        #endregion

        public override IEnumerator Initialize()
        {
            _model.InitId();

            if (_resourceLoader == null)
            {
                _resourceLoader = new ResourceLoader();
            }

            if (_view == null)
            {
                SetView(GetNewGoogleFormUploaderView());
            }

            SOGameConstants gameConstants = _resourceLoader.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS);
            if (gameConstants != null)
            {
                _model.SetAnalyticFormConstants(gameConstants.AnalyticFormConstants);
                _model.SetQuizFormConstants(gameConstants.QuizFormConstants);
                _model.SetReportFormConstants(gameConstants.ReportFormConstants);
            }
            else
            {
                Debug.LogError("GAMECONSTANT SCRIPTABLE NOT FOUND!");
            }

            yield return base.Initialize();
        }

        public GoogleFormUploaderView GetNewGoogleFormUploaderView()
        {
            GameObject obj = new GameObject(nameof(GoogleFormUploaderView));
            GameObject.DontDestroyOnLoad(obj);
            return obj.AddComponent<GoogleFormUploaderView>();
        }

        #region MESSAGE LISTENER

        public void OnSendChoicesRecords(ChoicesRecordsMessage message)
        {
            foreach (var choicesRecord in message.ChoicesRecords)
            {
                Dictionary<string, string> keyValues = new()
                {
                    { _model.QuizFormConstants.EntryIds.SessionId, _model.SessionId },
                    { _model.QuizFormConstants.EntryIds.DeviceId, _model.DeviceId },
                    { _model.QuizFormConstants.EntryIds.Question, choicesRecord.Question },
                    { _model.QuizFormConstants.EntryIds.Choices, choicesRecord.Choices },
                    { _model.QuizFormConstants.EntryIds.IsFirstChoice, choicesRecord.IsFirstChoice }
                };

                _view.SubmitForm(_model.QuizFormConstants.FormUrl, keyValues);
            }
        }

        public void OnSendAnalyticRecord(AnalyticRecordMessage message)
        {
            Dictionary<string, string> keyValues = new()
            {
                    { _model.AnalyticFormConstants.EntryIds.SessionId, _model.SessionId },
                    { _model.AnalyticFormConstants.EntryIds.DeviceId, _model.DeviceId },
                    { _model.AnalyticFormConstants.EntryIds.ScreenTimeInSeconds, message.AnalyticRecord.ScreenTimeInSeconds },
                    { _model.AnalyticFormConstants.EntryIds.AverageFps, message.AnalyticRecord.AverageFps },
                    { _model.AnalyticFormConstants.EntryIds.MaxFps, message.AnalyticRecord.MaxFps },
                    { _model.AnalyticFormConstants.EntryIds.MinFps, message.AnalyticRecord.MinFps },
                    { _model.AnalyticFormConstants.EntryIds.AverageMemory, message.AnalyticRecord.AverageMemory },
                    { _model.AnalyticFormConstants.EntryIds.MaxMemory, message.AnalyticRecord.MaxMemory },
                    { _model.AnalyticFormConstants.EntryIds.MinMemory, message.AnalyticRecord.MinMemory },
                    { _model.AnalyticFormConstants.EntryIds.LogWarningCount, message.AnalyticRecord.LogWarningCount },
                    { _model.AnalyticFormConstants.EntryIds.LogErrorCount, message.AnalyticRecord.LogErrorCount },
                    { _model.AnalyticFormConstants.EntryIds.LogExceptionCount, message.AnalyticRecord.LogExceptionCount },
                };

            _view.SubmitForm(_model.AnalyticFormConstants.FormUrl, keyValues);
        }

        public void OnSendReport(ReportMessage message)
        {
            Dictionary<string, string> keyValues = new()
            {
                    { _model.ReportFormConstants.SessionId, _model.SessionId },
                    { _model.ReportFormConstants.DeviceId, _model.DeviceId },
                    { _model.ReportFormConstants.MessageId, message.Message },
                };

            _view.SubmitForm(_model.ReportFormConstants.FormUrl, keyValues, message.Callback);
        }

        #endregion
    }
}