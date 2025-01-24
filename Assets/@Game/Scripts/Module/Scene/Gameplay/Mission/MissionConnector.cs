using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Mission

{
    public class MissionConnector : BaseConnector
    {
        private readonly MissionController _mission = new();

        protected override void Connect()
        {
            Subscribe<AdjustCollectedPuzzlePieceCountMessage>(_mission.OnAdjustCollectedPuzzlePieceCount);
            Subscribe<AdjustCollectedHiddenObjectCountMessage>(_mission.OnAdjustCollectedHiddenObjectCount);
            Subscribe<AdjustKillCountMessage>(_mission.OnAdjustKillCount);
            Subscribe<AdjustPadlockOnPlaceCountMessage>(_mission.OnAdjustPadlockOnPlaceCount);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AdjustCollectedPuzzlePieceCountMessage>(_mission.OnAdjustCollectedPuzzlePieceCount);
            Unsubscribe<AdjustCollectedHiddenObjectCountMessage>(_mission.OnAdjustCollectedHiddenObjectCount);
            Unsubscribe<AdjustKillCountMessage>(_mission.OnAdjustKillCount);
            Unsubscribe<AdjustPadlockOnPlaceCountMessage>(_mission.OnAdjustPadlockOnPlaceCount);
        }
    }
}