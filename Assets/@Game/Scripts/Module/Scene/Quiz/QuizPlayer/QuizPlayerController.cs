using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.QuizPlayer
{
    public class QuizPlayerController : ObjectController<QuizPlayerController, QuizPlayerModel, IQuizPlayerModel, QuizPlayerView>
    {
        public void SetModel(QuizPlayerModel model)
        {
            _model = model;
        }

        public override void SetView(QuizPlayerView view)
        {
            if (view.QuizItems == null)
            {
                Debug.LogError("QUIZITEMS IS NULL");
                return;
            }
            _model.InitQuizItem(view.QuizItems);
            _model.AddCallbacks(OnCorrect, OnWrong, OnDone);
            view.SetModel(_model);
            view.UpdateView();
        }

        public QuizPlayerView GetNewQuizPlayerView()
        {
            GameObject obj = new GameObject(nameof(QuizPlayerView));
            GameObject.DontDestroyOnLoad(obj);
            return obj.AddComponent<QuizPlayerView>();
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