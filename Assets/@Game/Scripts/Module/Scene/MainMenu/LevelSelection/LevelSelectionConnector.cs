using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionConnector : BaseConnector
    {
        private LevelSelectionController _levelSelection;

        protected override void Connect()
        {
            Subscribe<UnlockLevelMessage>(_levelSelection.OnUnlockLevel);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UnlockLevelMessage>(_levelSelection.OnUnlockLevel);
        }
    }
}