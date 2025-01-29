using Agate.MVC.Base;
using ProjectTA.Module.LevelSelection;
using UnityEngine;

namespace ProjectTA.Scene.LevelSelection
{
    public class LevelSelectionView : BaseSceneView
    {
        [field: SerializeField]
        public LevelSelectionPlayerView LevelSelectionPlayerView { get; private set; }
    }
}
