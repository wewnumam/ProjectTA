using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Mission

{
    public class MissionConnector : BaseConnector
    {
        private MissionController _mission;

        protected override void Connect()
        {
            Subscribe<AddCollectedPuzzlePieceCountMessage>(_mission.OnAddCollectedPuzzlePiece);
            Subscribe<SubtractCollectedPuzzlePieceCountMessage>(_mission.OnSubtractCollectedPuzzlePiece);
            Subscribe<AddKillCountMessage>(_mission.OnAddKillCount);
            Subscribe<SubtractKillCountMessage>(_mission.OnSubtractKillCount);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AddCollectedPuzzlePieceCountMessage>(_mission.OnAddCollectedPuzzlePiece);
            Unsubscribe<SubtractCollectedPuzzlePieceCountMessage>(_mission.OnSubtractCollectedPuzzlePiece);
            Unsubscribe<AddKillCountMessage>(_mission.OnAddKillCount);
            Unsubscribe<SubtractKillCountMessage>(_mission.OnSubtractKillCount);
        }
    }
}