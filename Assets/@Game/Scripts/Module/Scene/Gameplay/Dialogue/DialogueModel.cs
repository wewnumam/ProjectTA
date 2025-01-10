using Agate.MVC.Base;
using Cinemachine;
using Ink.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.Dialogue
{
    public class DialogueModel : BaseModel, IDialogueModel
    {
        public Story Story { get; private set; } = null;
        public string CharacterName { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public bool IsTextAnimationComplete { get; private set; } = true;
        public UnityAction OnTextAnimationComplete { get; private set; } = null;

        private string _currentLineText = String.Empty;
        
        public void InitStory(TextAsset textAsset)
        {
            Story = new Story(textAsset.text);
        }

        public void SetNextLine()
        {
            _currentLineText = Story.Continue();
        }

        public void SetIsTextAnimationComplete(bool isTextAnimationComplete)
        {
            IsTextAnimationComplete = isTextAnimationComplete;
        }

        public void UpdateDialogueLine()
        {
            string characterName = string.Empty;
            string message = _currentLineText?.Trim();
            
            // Parse dialogue line
            if (message != null && message.Contains(":"))
            {
                int endIndex = message.IndexOf(':');
                characterName = message.Substring(0, endIndex);
                message = message.Replace(characterName + ": ", "");
            }

            CharacterName = characterName;
            Message = message;
            OnTextAnimationComplete = () => IsTextAnimationComplete = true;

            SetDataAsDirty();
        }

        public string GetLog()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("CutscenePlayerModel Log:");
            sb.AppendLine($"{nameof(CharacterName)}: {CharacterName}");
            sb.AppendLine($"{nameof(Message)}: {Message}");
            sb.AppendLine($"{nameof(IsTextAnimationComplete)}: {IsTextAnimationComplete}");
            sb.AppendLine($"{nameof(OnTextAnimationComplete)}: {(OnTextAnimationComplete != null ? "Set" : "Not Set")}");

            return sb.ToString();
        }
    }
}