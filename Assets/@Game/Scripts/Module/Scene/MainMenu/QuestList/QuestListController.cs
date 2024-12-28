using Agate.MVC.Base;
using ProjectTA.Module.QuestData;
using ProjectTA.Scene.QuestList;
using ProjectTA.Utility;
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

            float points = 0;

            foreach (var questItem in _questCollection.QuestItems)
            {
                GameObject obj = GameObject.Instantiate(view.QuestComponentTemplate.gameObject, view.Parent);
                QuestComponent questComponent = obj.GetComponent<QuestComponent>();
                questComponent.LabelText.SetText(questItem.Label);
                questComponent.Slider.maxValue = questItem.RequiredAmount;

                float currentAmount = GetCurrentAmount(questItem);
                float progress = currentAmount / questItem.RequiredAmount * 100f;

                points += progress > 100f ? 100f : progress;

                questComponent.Slider.value = currentAmount;
                questComponent.ProgressText.SetText($"{currentAmount}/{questItem.RequiredAmount}");
                obj.SetActive(true);
            }

            view.PointsText.SetText($"{(int)points}");
        }

        private float GetCurrentAmount(QuestItem questItem)
        {
            float currentAmount = 0;

            switch (questItem.Type)
            {
                case EnumManager.QuestType.Kill:
                    currentAmount = _questData.CurrentKillAmount;
                    break;
                case EnumManager.QuestType.Collectible:
                    currentAmount = _questData.CurrentCollectibleAmount;
                    break;
                case EnumManager.QuestType.Puzzle:
                    currentAmount = _questData.CurrentPuzzleAmount;
                    break;
                case EnumManager.QuestType.HiddenObject:
                    currentAmount = _questData.CurrentHiddenObjectAmount;
                    break;
                case EnumManager.QuestType.LevelPlayed:
                    currentAmount = _questData.CurrentLevelPlayedAmount;
                    break;
                case EnumManager.QuestType.GameWin:
                    currentAmount = _questData.CurrentGameWinAmount;
                    break;
                case EnumManager.QuestType.MinutesPlayed:
                    currentAmount = _questData.CurrentMinutesPlayedAmount;
                    break;
                case EnumManager.QuestType.QuizScore:
                    currentAmount = _questData.CurrentQuizScoreAmount;
                    break;
            }

            return currentAmount;
        }
    }
}