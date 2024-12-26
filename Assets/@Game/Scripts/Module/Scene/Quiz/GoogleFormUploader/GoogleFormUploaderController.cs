using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using System.Collections.Generic;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderController : ObjectController<GoogleFormUploaderController, GoogleFormUploaderView>
    {
        private QuizFormConstants _quizFormConstants;

        public void SetQuizFormConstants(QuizFormConstants quizFormConstants)
        {
            _quizFormConstants = quizFormConstants;
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
    }
}