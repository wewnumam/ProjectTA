using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.QuestData
{
    public class QuestDataController : DataController<QuestDataController, QuestDataModel, IQuestDataModel>
    {
        private string _fileName = "QuestCollection";

        public void SetFileName(string fileName)
        {
            _fileName = fileName;
        }

        public void SetModel(QuestDataModel model)
        {
            _model = model;
        }

        public void SetCurrentQuestData(QuestData questData)
        {
            _model.SetCurrentQuestData(questData);
        }

        public void SetCollectibleCollectionAndUnlockedCollectible(SOCollectibleCollection collectibleCollection, List<string> unlockedCollectible)
        {
            _model.SetCurrentCollectibleAmount(unlockedCollectible.Count);
            _model.SetCurrentPuzzleAmount(_model.GetCurrentCollectibleByTypeAmount(collectibleCollection.CollectibleItems, unlockedCollectible, EnumManager.CollectibleType.Puzzle));
            _model.SetCurrentHiddenObjectAmount(_model.GetCurrentCollectibleByTypeAmount(collectibleCollection.CollectibleItems, unlockedCollectible, EnumManager.CollectibleType.HiddenObject));
        }

        public void SetCurrentLevelPlayedAmount(int count)
        {
            _model.SetCurrentLevelPlayedAmount(count);
        }

        public override IEnumerator Initialize()
        {
            try
            {
                SOQuestCollection gameConstants = Resources.Load<SOQuestCollection>(_fileName);
                _model.SetGameConstants(gameConstants);
            }
            catch (Exception e)
            {
                Debug.LogError("QUESTCOLLECTION SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            yield return base.Initialize();
        }

        public void OnGameOver(GameOverMessage message)
        {
            _model.AddCurrentMinutesPlayedAmount(_model.CurrentSessionInMinutes);
            Publish(new UpdateQuestDataMessage(_model.CurrentQuestData));
        }

        public void OnGameWin(GameWinMessage message)
        {
            _model.AddCurrentMinutesPlayedAmount(_model.CurrentSessionInMinutes);
            _model.AddCurrentGameWinAmount(1);
            Publish(new UpdateQuestDataMessage(_model.CurrentQuestData));
        }

        public void OnAddKillCount(AddKillCountMessage message)
        {
            _model.AddCurrentKillAmount(message.Amount);
        }

        public void OnSetQuizScore(QuizScoreMessage message)
        {
            _model.SetCurrentQuizScoreAmount(message.Score);
            Publish(new UpdateQuestDataMessage(_model.CurrentQuestData));
        }

        public void OnUpdateCountdown(UpdateCountdownMessage message)
        {
            _model.SetCurrentSessionPlayed(message.InitialCountdown - message.CurrentCountdown);
        }
    }
}
