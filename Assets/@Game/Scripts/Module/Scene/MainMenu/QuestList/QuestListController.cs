using Agate.MVC.Base;
using ProjectTA.Module.QuestData;
using ProjectTA.Scene.QuestList;
using System;
using UnityEngine;

namespace ProjectTA.Module.QuestList
{
    public class QuestListController : ObjectController<QuestListController, QuestListView>
    {
        private SOQuestCollection _questCollection = null;
        private QuestData.QuestData _questData = null;

        public void SetQuestCollection(SOQuestCollection questCollection)
        {
            _questCollection = questCollection;
        }

        public void SetQuestData(QuestData.QuestData questData)
        {
            _questData = questData;
        }

        public override void SetView(QuestListView view)
        {
            base.SetView(view);

            foreach (var questItem in _questCollection.QuestItems)
            {
                GameObject obj = GameObject.Instantiate(view.QuestComponentTemplate.gameObject, view.Parent);
                QuestComponent questComponent = obj.GetComponent<QuestComponent>();
                questComponent.LabelText.SetText(questItem.Label);
                questComponent.Slider.maxValue = questItem.RequiredAmount;

                float currentAmount = 0;

                if (questItem.Type == Utility.EnumManager.QuestType.Kill)
                {
                    currentAmount = _questData.CurrentKillAmount;
                }

                questComponent.Slider.value = currentAmount;
                questComponent.ProgressText.SetText($"{currentAmount}/{questItem.RequiredAmount}");
                obj.SetActive(true);
            }
        }
    }
}