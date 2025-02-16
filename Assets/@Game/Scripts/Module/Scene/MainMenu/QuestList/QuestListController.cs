using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.QuestData;
using ProjectTA.Utility;
using System;
using UnityEngine;

namespace ProjectTA.Module.QuestList
{
    public class QuestListController : ObjectController<QuestListController, QuestDataModel, QuestListView>
    {
        public void SetModel(QuestDataModel model)
        {
            _model = model;
        }

        public void InitModel(IQuestDataModel questData)
        {
            if (questData == null)
            {
                Debug.LogError("QUESTDATA IS NULL");
                return;
            }
            if (questData.QuestCollection == null)
            {
                Debug.LogError("QUESTCOLLECTION IS NULL");
                return;
            }

            _model.SetQuestCollection(questData.QuestCollection);

            if (questData.CurrentQuestData == null)
            {
                Debug.LogError("CURRENTQUESTDATA IS NULL");
                return;
            }

            _model.SetCurrentQuestData(questData.CurrentQuestData);
        }

        public override void SetView(QuestListView view)
        {
            base.SetView(view);
            view.SetCallback(OnPlayLastCutscene);

            float points = 0;

            foreach (var questItem in _model.QuestCollection.QuestItems)
            {
                GameObject obj = GameObject.Instantiate(view.QuestComponentTemplate.gameObject, view.Parent);
                QuestComponent questComponent = obj.GetComponent<QuestComponent>();
                questComponent.LabelText.SetText(questItem.Label);
                questComponent.Slider.maxValue = questItem.RequiredAmount;

                float currentAmount = _model.GetCurrentAmount(questItem);
                float progress = Mathf.Min(currentAmount / questItem.RequiredAmount * 100f, 100f); // Cap progress at 100%
                points += progress;

                questComponent.Slider.value = currentAmount;
                questComponent.ProgressText.SetText($"{currentAmount}/{questItem.RequiredAmount}");

                obj.SetActive(true);
            }

            view.PointsText.SetText($"{(int)points}");
            if (_model.IsQuestComplete())
            {
                view.OnQuestComplete?.Invoke();
            }
        }

        private void OnPlayLastCutscene()
        {
            if (_model.QuestCollection.LastCutscene == null)
            {
                Debug.LogError("QUESTCOLLECTION LASTCUTSCENE IS NULL");
                return;
            }

            Publish(new ChooseCutsceneMessage(_model.QuestCollection.LastCutscene));
            SceneLoader.Instance.LoadScene(TagManager.SCENE_CUTSCENE);
        }
    }
}