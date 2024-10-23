using ProjectTA.Message;
using Agate.MVC.Base;

namespace ProjectTA.Module.Dialogue
{
    public class DialogueConnector : BaseConnector
    {
        private DialogueController _dialogue;

        protected override void Connect()
        {
            Subscribe<ShowDialogueMessage>(_dialogue.ShowDialogue);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ShowDialogueMessage>(_dialogue.ShowDialogue);
        }
    }
}