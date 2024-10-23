using UnityEngine;

namespace ProjectTA.Message
{
    public struct ShowDialogueMessage
    {
        public TextAsset TextAsset { get; }

        public ShowDialogueMessage(TextAsset textAsset)
        {
            TextAsset = textAsset;
        }
    }
}