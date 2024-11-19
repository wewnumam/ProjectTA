using Agate.MVC.Base;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerModel : BaseModel, ICutscenePlayerModel
    {
        public SO_CutsceneData CurrentCutsceneData { get; private set; }

        public void SetCurrentCutsceneData(SO_CutsceneData cutsceneData)
        {
            CurrentCutsceneData = cutsceneData;
            SetDataAsDirty();
        }
    }
}