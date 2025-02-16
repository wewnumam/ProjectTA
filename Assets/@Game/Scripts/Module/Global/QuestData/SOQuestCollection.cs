using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.QuestData
{
    [CreateAssetMenu(fileName = "QuestCollection", menuName = "ProjectTA/QuestCollection", order = 5)]
    public class SOQuestCollection : ScriptableObject
    {
        [SerializeField] List<QuestItem> _questItems;
        [field:SerializeField]
        public SOCollectibleData LastCollectible {  get; set; }
        [field:SerializeField]
        public SOCutsceneData LastCutscene {  get; set; }

        public List<QuestItem> QuestItems { get => _questItems; set => _questItems = value; }
    }

    [Serializable]
    public class QuestItem
    {
        [SerializeField] private string _label;
        [SerializeField] private EnumManager.QuestType _type;
        [SerializeField] private float _requiredAmount;

        public string Label { get => _label; set => _label = value; }
        public EnumManager.QuestType Type { get => _type; set => _type = value; }
        public float RequiredAmount { get => _requiredAmount; set => _requiredAmount = value; }
    }
}
