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
        private SaveSystem<SavedQuestData> _savedQuestData = null;
        private SaveSystem<SavedUnlockedCollectibles> _savedUnlockedCollectibles = null;

        public void SetCollectibleCollectionAndUnlockedCollectible(SOCollectibleCollection collectibleCollection, List<string> unlockedCollectible)
        {
            _model.SetCurrentCollectibleAmount(unlockedCollectible.Count);
            _model.SetCurrentPuzzleAmount(_model.GetCurrentCollectibleByTypeAmount(collectibleCollection.CollectibleItems, unlockedCollectible, EnumManager.CollectibleType.Puzzle));
            _model.SetCurrentHiddenObjectAmount(_model.GetCurrentCollectibleByTypeAmount(collectibleCollection.CollectibleItems, unlockedCollectible, EnumManager.CollectibleType.HiddenObject));
        }

        public override IEnumerator Initialize()
        {
            _savedQuestData = new(TagManager.FILENAME_QUESTDATA, true);
            _model.SetCurrentQuestData(_savedQuestData.Load());

            _savedUnlockedCollectibles = new(TagManager.FILENAME_SAVEDUNLOCKEDCOLLECTIBLES, true);
            _model.SetUnlockedCollectibles(_savedUnlockedCollectibles.Load());

            try
            {
                SOQuestCollection gameConstants = Resources.Load<SOQuestCollection>(@"QuestCollection");
                _model.SetQuestCollection(gameConstants);
            }
            catch (Exception e)
            {
                Debug.LogError("QUESTCOLLECTION SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            try
            {
                SOCollectibleCollection collectibleCollection = Resources.Load<SOCollectibleCollection>(@"CollectibleCollection");
                SetCollectibleCollectionAndUnlockedCollectible(collectibleCollection, _model.UnlockedCollectibles.Items);
            }
            catch (Exception e)
            {
                Debug.LogError("QUESTCOLLECTION SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            yield return base.Initialize();
        }

        public void OnAddLevelPlayed(AddLevelPlayedMessage message)
        {
            _model.AddLevelPlayed(message.LevelName);

            _savedQuestData.Save(_model.CurrentQuestData);
        }

        public void OnGameOver(GameOverMessage message)
        {
            _model.AddCurrentMinutesPlayedAmount(_model.CurrentSessionInMinutes);

            _savedQuestData.Save(_model.CurrentQuestData);
        }

        public void OnGameWin(GameWinMessage message)
        {
            _model.AddCurrentMinutesPlayedAmount(_model.CurrentSessionInMinutes);
            _model.AddCurrentGameWinAmount(1);

            _savedQuestData.Save(_model.CurrentQuestData);
        }

        public void OnAddKillCount(AddKillCountMessage message)
        {
            _model.AddCurrentKillAmount(message.Amount);
        }

        public void OnSetQuizScore(QuizScoreMessage message)
        {
            _model.SetCurrentQuizScoreAmount(message.Score);

            _savedQuestData.Save(_model.CurrentQuestData);
        }

        public void OnUpdateCountdown(UpdateCountdownMessage message)
        {
            _model.SetCurrentSessionPlayed(message.InitialCountdown - message.CurrentCountdown);
        }

        public void OnDeleteSaveData(DeleteSaveDataMessage message)
        {
            _savedQuestData.Delete();
        }
    }
}
