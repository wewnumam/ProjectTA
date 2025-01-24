using Agate.MVC.Base;
using Cinemachine;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.CutscenePlayer
{
    public interface ICutscenePlayerModel : IBaseModel
    {
        Story Story { get; }
        string CharacterName { get; }
        string Message { get; set; }
        bool IsTextAnimationComplete { get; }

        UnityAction OnTextAnimationComplete { get; }

        void InitStory(TextAsset textAsset);
        void SetCameras(List<CinemachineVirtualCamera> cameras);
        void SetNextLine();
        void UpdateDialogueLine();
        void SetIsTextAnimationComplete(bool v);
        void GoNextCamera();
        string GetLog();
    }
}