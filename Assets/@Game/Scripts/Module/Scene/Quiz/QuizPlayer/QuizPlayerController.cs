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
            view.SetModel(_model);
            view.UpdateView();
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
            Publish(new QuizScoreMessage(_model.GetScore()));
            SceneLoader.Instance.LoadScene(TagManager.SCENE_MAINMENU);
        }
    }
}