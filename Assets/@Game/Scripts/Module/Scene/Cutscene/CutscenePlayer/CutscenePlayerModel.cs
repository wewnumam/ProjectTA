using Agate.MVC.Base;
using Cinemachine;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerModel : BaseModel
    {
        public SOCutsceneData CurrentCutsceneData { get; private set; }
        public List<CinemachineVirtualCamera> Cameras { get; private set; }

        public void SetCurrentCutsceneData(SOCutsceneData cutsceneData)
        {
            CurrentCutsceneData = cutsceneData;
            SetDataAsDirty();
        }

        public void SetCameras(List<CinemachineVirtualCamera> cameras)
        {
            Cameras = cameras;
            SetDataAsDirty();
        }
    }
}