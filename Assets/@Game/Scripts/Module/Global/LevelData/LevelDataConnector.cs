using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.LevelData
{
    public class LevelDataConnector : BaseConnector
    {
        private LevelDataController _levelData;

        protected override void Connect()
        {
            Subscribe<ChooseLevelMessage>(_levelData.OnChooseLevel);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ChooseLevelMessage>(_levelData.OnChooseLevel);
        }
    }
}