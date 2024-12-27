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

        public List<QuestItem> QuestItems => _questItems;
    }

    [Serializable]
    public class QuestItem
    {
        [SerializeField] private string _label;
        [SerializeField] private EnumManager.QuestType _type;
        [SerializeField] private float _requiredAmount;

        public string Label => _label;
        public EnumManager.QuestType Type => _type;
        public float RequiredAmount => _requiredAmount;
    }
}
