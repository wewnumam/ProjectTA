using NaughtyAttributes;
using ProjectTA.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.LevelData
{
    [CreateAssetMenu(fileName = "CutsceneData_", menuName = "ProjectTA/CutsceneData", order = 2)]
    public class SO_CutsceneData : ScriptableObject
    {
        public TextAsset dialogueAsset;
        [ShowAssetPreview]
        public List<Sprite> sceneSprites;

        public GameObject environment;
    }
}
