using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Utility;

namespace ProjectTA.Module.QuizPlayer
{
    public class QuizPlayerController : ObjectController<QuizPlayerController, QuizPlayerModel, IQuizPlayerModel, QuizPlayerView>
    {
        public override void SetView(QuizPlayerView view)
        {
            _model.InitQuizItem(view.QuizItems);
            _model.AddCallbacks(OnCorrect, OnWrong, OnDone);
            _model.SetNextQuizItem();
            view.SetModel(_model);
        }

        private void OnCorrect()
        {
            _model.SetNextQuizItem();
        }

        private void OnWrong()
        {
            _model.AddWrongCount();
        }

        private void OnDone()
        {
            Publish(new ChoicesRecordsMessage(_model.ChoicesRecords));
            SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);
        }
    }
}