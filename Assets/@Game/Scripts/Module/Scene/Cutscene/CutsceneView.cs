using Agate.MVC.Base;
using ProjectTA.Module.CutscenePlayer;
using UnityEngine;

namespace ProjectTA.Scene.Cutscene
{
    public class CutsceneView : BaseSceneView
    {
        [field:SerializeField]
        public CutscenePlayerView CutscenePlayerView { get; private set; }
    }
}
