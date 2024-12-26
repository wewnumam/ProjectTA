using Agate.MVC.Base;
using ProjectTA.Module.GoogleFormUploader;
using ProjectTA.Module.QuizPlayer;
using UnityEngine;

namespace ProjectTA.Scene.Quiz
{
    public class QuizView : BaseSceneView
    {
        [SerializeField] private QuizPlayerView _quizPlayerView;
        [SerializeField] private GoogleFormUploaderView _googleFormUploaderView;

        public QuizPlayerView QuizPlayerView => _quizPlayerView;
        public GoogleFormUploaderView GoogleFormUploaderView => _googleFormUploaderView;
    }
}
