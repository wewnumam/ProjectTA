using ProjectTA.Module.QuizPlayer;
using System.Collections.Generic;

namespace ProjectTA.Message
{
    public struct ChoicesRecordsMessage
    {
        public List<ChoicesRecord> ChoicesRecords;

        public ChoicesRecordsMessage(List<ChoicesRecord> choicesRecords)
        {
            ChoicesRecords = choicesRecords;
        }
    }
}