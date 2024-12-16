using Agate.MVC.Base;
using ProjectTA.Module.CutscenePlayer;
using UnityEngine;

namespace ProjectTA.Scene.Cutscene
{
    public class CutsceneView : BaseSceneView
    {
        [SerializeField] private CutscenePlayerView _cutscenePlayerView;

        public CutscenePlayerView CutscenePlayerView => _cutscenePlayerView;
    }
}
