using Agate.MVC.Base;
using ProjectTA.Module.QuestData;
using ProjectTA.Utility;
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
            float points = 0;

            foreach (var questItem in _model.QuestCollection.QuestItems)
            {
                GameObject obj = GameObject.Instantiate(view.QuestComponentTemplate.gameObject, view.Parent);
                QuestComponent questComponent = obj.GetComponent<QuestComponent>();
                questComponent.LabelText.SetText(questItem.Label);
                questComponent.Slider.maxValue = questItem.RequiredAmount;

                float currentAmount = GetCurrentAmount(questItem);
                float progress = Mathf.Min(currentAmount / questItem.RequiredAmount * 100f, 100f); // Cap progress at 100%
                points += progress;

                questComponent.Slider.value = currentAmount;
                questComponent.ProgressText.SetText($"{currentAmount}/{questItem.RequiredAmount}");

                obj.SetActive(true);
            }

            view.PointsText.SetText($"{(int)points}");
        }

        public float GetCurrentAmount(QuestItem questItem)
        {
            return questItem.Type switch
            {
                EnumManager.QuestType.Kill => _model.CurrentQuestData.CurrentKillAmount,
                EnumManager.QuestType.Collectible => _model.CurrentQuestData.CurrentCollectibleAmount,
                EnumManager.QuestType.Puzzle => _model.CurrentQuestData.CurrentPuzzleAmount,
                EnumManager.QuestType.HiddenObject => _model.CurrentQuestData.CurrentHiddenObjectAmount,
                EnumManager.QuestType.LevelPlayed => _model.CurrentQuestData.LevelPlayed.Count,
                EnumManager.QuestType.GameWin => _model.CurrentQuestData.CurrentGameWinAmount,
                EnumManager.QuestType.MinutesPlayed => _model.CurrentQuestData.CurrentMinutesPlayedAmount,
                EnumManager.QuestType.QuizScore => _model.CurrentQuestData.CurrentQuizScoreAmount,
                _ => 0
            };
        }
    }
}