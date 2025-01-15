using Agate.MVC.Base;
using Ink.Runtime;
using UnityEngine.Events;

namespace ProjectTA.Module.Dialogue
{
    public interface IDialogueModel : IBaseModel
    {
        Story Story { get; }
        string CharacterName { get; }
        string Message { get; }
        bool IsTextAnimationComplete { get; }

        UnityAction OnTextAnimationComplete { get; }

        string GetLog();
    }
}