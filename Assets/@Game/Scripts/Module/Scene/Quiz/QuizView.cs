using Agate.MVC.Base;
using ProjectTA.Module.QuizPlayer;
using UnityEngine;

namespace ProjectTA.Scene.Quiz
{
    public class QuizView : BaseSceneView
    {
        [field: SerializeField]
        public QuizPlayerView QuizPlayerView { get; private set; }
    }
}
