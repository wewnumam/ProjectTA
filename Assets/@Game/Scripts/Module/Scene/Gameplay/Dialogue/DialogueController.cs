using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;

namespace ProjectTA.Module.Dialogue
{
    public class DialogueController : ObjectController<DialogueController, DialogueModel, IDialogueModel, DialogueView>
    {
        public override void SetView(DialogueView view)
        {
            base.SetView(view);
            view.SetCallback(DisplayNextLine);
        }

        public void DisplayNextLine()
        {
            if (!_model.Story.canContinue)
            {
                Publish(new GameResumeMessage());
                Publish(new GameStateMessage(EnumManager.GameState.Playing));
                _view.OnEnd?.Invoke();
                return;
            }

            if (_model.IsTextAnimationComplete)
            {
                _model.SetNextLine();
                _model.UpdateDialogueLine();
                _model.SetIsTextAnimationComplete(false);
                if (!_model.Story.canContinue)
                {
                    _view.OnLastLine?.Invoke();
                }
            }
            else
            {
                _model.UpdateDialogueLine();
                _model.SetIsTextAnimationComplete(true);
            }
        }

        public void ShowDialogue(ShowDialogueMessage message)
        {
            if (message.TextAsset == null)
                return;

            Publish(new GamePauseMessage());
            Publish(new GameStateMessage(EnumManager.GameState.Dialogue));

            _model.InitStory(message.TextAsset);
            DisplayNextLine();
            _view.OnStart?.Invoke();

        }
    }
}
