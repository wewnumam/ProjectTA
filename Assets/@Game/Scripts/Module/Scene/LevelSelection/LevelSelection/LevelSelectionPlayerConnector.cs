using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerConnector : BaseConnector
    {
        private LevelSelectionPlayerController _levelSelection;

        protected override void Connect()
        {
            Subscribe<ChooseLevelMessage>(_levelSelection.OnChooseLevel);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseLevelMessage>(_levelSelection.OnChooseLevel);
        }
    }
}