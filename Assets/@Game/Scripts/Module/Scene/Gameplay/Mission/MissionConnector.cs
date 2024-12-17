using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Mission

{
    public class MissionConnector : BaseConnector
    {
        private readonly MissionController _mission = new();

        protected override void Connect()
        {
            Subscribe<AddCollectedPuzzlePieceCountMessage>(_mission.OnAddCollectedPuzzlePiece);
            Subscribe<AddCollectedHiddenObjectCountMessage>(_mission.OnAddCollectedHidenObject);
            Subscribe<SubtractCollectedPuzzlePieceCountMessage>(_mission.OnSubtractCollectedPuzzlePiece);
            Subscribe<AddKillCountMessage>(_mission.OnAddKillCount);
            Subscribe<SubtractKillCountMessage>(_mission.OnSubtractKillCount);
            Subscribe<AddPadlockOnPlaceMessage>(_mission.OnAddPadlockOnPlace);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AddCollectedPuzzlePieceCountMessage>(_mission.OnAddCollectedPuzzlePiece);
            Subscribe<AddCollectedHiddenObjectCountMessage>(_mission.OnAddCollectedHidenObject);
            Unsubscribe<SubtractCollectedPuzzlePieceCountMessage>(_mission.OnSubtractCollectedPuzzlePiece);
            Unsubscribe<AddKillCountMessage>(_mission.OnAddKillCount);
            Unsubscribe<SubtractKillCountMessage>(_mission.OnSubtractKillCount);
            Unsubscribe<AddPadlockOnPlaceMessage>(_mission.OnAddPadlockOnPlace);
        }
    }
}