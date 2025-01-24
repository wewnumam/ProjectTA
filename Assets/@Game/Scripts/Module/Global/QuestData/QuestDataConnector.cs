using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.QuestData
{
    public class QuestDataConnector : BaseConnector
    {
        private readonly QuestDataController _questData = new();

        protected override void Connect()
        {
            Subscribe<AddLevelPlayedMessage>(_questData.OnAddLevelPlayed);
            Subscribe<GameOverMessage>(_questData.OnGameOver);
            Subscribe<GameWinMessage>(_questData.OnGameWin);
            Subscribe<AdjustKillCountMessage>(_questData.OnAddKillCount);
            Subscribe<QuizScoreMessage>(_questData.OnSetQuizScore);
            Subscribe<UpdateCountdownMessage>(_questData.OnUpdateCountdown);
            Subscribe<DeleteSaveDataMessage>(_questData.OnDeleteSaveData);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AddLevelPlayedMessage>(_questData.OnAddLevelPlayed);
            Unsubscribe<GameOverMessage>(_questData.OnGameOver);
            Unsubscribe<GameWinMessage>(_questData.OnGameWin);
            Unsubscribe<AdjustKillCountMessage>(_questData.OnAddKillCount);
            Unsubscribe<QuizScoreMessage>(_questData.OnSetQuizScore);
            Unsubscribe<UpdateCountdownMessage>(_questData.OnUpdateCountdown);
            Unsubscribe<DeleteSaveDataMessage>(_questData.OnDeleteSaveData);
        }
    }
}