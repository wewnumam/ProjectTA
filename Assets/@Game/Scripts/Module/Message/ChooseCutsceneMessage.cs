using ProjectTA.Module.LevelData;

namespace ProjectTA.Message
{
    public struct ChooseCutsceneMessage
    {
        public SOCutsceneData CutsceneData { get; }

        public ChooseCutsceneMessage(SOCutsceneData cutsceneData)
        {
            CutsceneData = cutsceneData;
        }
    }
}