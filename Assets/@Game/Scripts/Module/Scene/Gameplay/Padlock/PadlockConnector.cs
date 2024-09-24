using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Padlock
{
    public class PadlockConnector : BaseConnector
    {
        private PadlockController _padlock;

        protected override void Connect()
        {
            Subscribe<ShowPadlockMessage>(_padlock.ShowPadlock);
            Subscribe<UpdatePuzzleCountMessage>(_padlock.OnUpdatePuzzleCount);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ShowPadlockMessage>(_padlock.ShowPadlock);
            Unsubscribe<UpdatePuzzleCountMessage>(_padlock.OnUpdatePuzzleCount);
        }
    }
}