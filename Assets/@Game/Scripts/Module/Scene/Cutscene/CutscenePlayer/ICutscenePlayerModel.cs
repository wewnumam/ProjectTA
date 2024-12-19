using Agate.MVC.Base;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.CutscenePlayer
{
    public interface ICutscenePlayerModel : IBaseModel
    {
        Story Story { get; }
        string CharacterName {  get; }
        string Message {  get; }
        bool IsTextAnimationComplete { get; }

        UnityAction OnTextAnimationComplete { get; }

        string GetLog();
    }
}