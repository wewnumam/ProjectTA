using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CutscenePlayer
{
    public interface ICutscenePlayerModel : IBaseModel
    {
        SO_CutsceneData CurrentCutsceneData { get; }
    }
}