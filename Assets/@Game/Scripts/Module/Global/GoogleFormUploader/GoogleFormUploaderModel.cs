using Agate.MVC.Base;
using ProjectTA.Module.GameConstants;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderModel : BaseModel
    {
        public AnalyticFormConstants AnalyticFormConstants { get; private set; } = null;
        public QuizFormConstants QuizFormConstants { get; private set; } = null;

        public void SetAnalyticFormConstants(AnalyticFormConstants analyticFormConstants)
        {
            AnalyticFormConstants = analyticFormConstants;
        }

        public void SetQuizFormConstants(QuizFormConstants quizFormConstants)
        {
            QuizFormConstants = quizFormConstants;
        }
    }
}