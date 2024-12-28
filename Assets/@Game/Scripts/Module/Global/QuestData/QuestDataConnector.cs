using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.QuestData
{
    public class QuestDataConnector : BaseConnector
    {
        private readonly QuestDataController _questData = new();

        protected override void Connect()
        {
            Subscribe<GameOverMessage>(_questData.OnGameOver);
            Subscribe<GameWinMessage>(_questData.OnGameWin);
            Subscribe<AddKillCountMessage>(_questData.OnAddKillCount);
            Subscribe<QuizScoreMessage>(_questData.OnSetQuizScore);
            Subscribe<UpdateCountdownMessage>(_questData.OnUpdateCountdown);
        }

        protected override void Disconnect()
        {
            Unsubscribe<GameOverMessage>(_questData.OnGameOver);
            Unsubscribe<GameWinMessage>(_questData.OnGameWin);
            Unsubscribe<AddKillCountMessage>(_questData.OnAddKillCount);
            Unsubscribe<QuizScoreMessage>(_questData.OnSetQuizScore);
            Unsubscribe<UpdateCountdownMessage>(_questData.OnUpdateCountdown);
        }
    }
}