using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Message
{
    public struct LoadProgressMessage
    {
        public string Label { get; }
        public float Percentage { get; }
        public bool IsDone { get; }

        public LoadProgressMessage(string label, float percentage, bool isDone)
        {
            Label = label;
            Percentage = percentage;
            IsDone = isDone;
        }
    }
}