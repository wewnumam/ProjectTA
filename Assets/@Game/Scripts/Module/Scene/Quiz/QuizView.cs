using Agate.MVC.Base;
using ProjectTA.Module.QuizPlayer;
using UnityEngine;

namespace ProjectTA.Scene.Quiz
{
    public class QuizView : BaseSceneView
    {
        [SerializeField] private QuizPlayerView _quizPlayerView;

        public QuizPlayerView QuizPlayerView => _quizPlayerView;
    }
}
