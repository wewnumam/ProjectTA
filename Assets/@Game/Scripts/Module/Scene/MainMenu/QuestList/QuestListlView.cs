using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.QuestList
{
    public class QuestListView : BaseView
    {
        [field: SerializeField]
        public Transform Parent { get; set; }
        [field: SerializeField]
        public QuestComponent QuestComponentTemplate { get; set; }
        [field: SerializeField]
        public TMP_Text PointsText { get; set; }
        [field: SerializeField]
        public UnityEvent OnQuestComplete { get; set; }

        private UnityAction _onPlayCutscene;

        public void SetCallback(UnityAction onPlayCutscene)
        {
            _onPlayCutscene = onPlayCutscene;
        }

        public void PlayFinalCutscene()
        {
            _onPlayCutscene?.Invoke();
        }
    }
}