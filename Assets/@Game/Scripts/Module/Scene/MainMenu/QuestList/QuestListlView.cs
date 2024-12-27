using Agate.MVC.Base;
using ProjectTA.Scene.QuestList;
using UnityEngine;

namespace ProjectTA.Module.QuestList
{
    public class QuestListView : BaseView
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private QuestComponent _questComponentTemplate;
        
        public QuestComponent QuestComponentTemplate => _questComponentTemplate;
        public Transform Parent => _parent;
    }
}