using Agate.MVC.Base;
using ProjectTA.Scene.QuestList;
using TMPro;
using UnityEngine;

namespace ProjectTA.Module.QuestList
{
    public class QuestListView : BaseView
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private QuestComponent _questComponentTemplate;
        [SerializeField] private TMP_Text _pointsText;

        public QuestComponent QuestComponentTemplate => _questComponentTemplate;
        public Transform Parent => _parent;
        public TMP_Text PointsText => _pointsText;
    }
}