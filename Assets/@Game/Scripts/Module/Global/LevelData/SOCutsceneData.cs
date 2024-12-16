using ProjectTA.Module.CutscenePlayer;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "CutsceneData_", menuName = "ProjectTA/CutsceneData", order = 2)]
    public class SOCutsceneData : ScriptableObject
    {
        [SerializeField] private TextAsset _dialogueAsset;
        [SerializeField] private CutsceneComponent _environment;

        public TextAsset DialogueAsset => _dialogueAsset;
        public CutsceneComponent Environment => _environment;
    }
}
