using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.QuestData
{
    public class QuestDataController : DataController<QuestDataController, QuestDataModel, IQuestDataModel>
    {
        #region UTILITY

        private ISaveSystem<SavedQuestData> _savedQuestData = null;
        private ISaveSystem<SavedUnlockedCollectibles> _savedUnlockedCollectibles = null;
        private IResourceLoader _resourceLoader = null;

        public void SetSaveSystem(ISaveSystem<SavedQuestData> savedQuestData, ISaveSystem<SavedUnlockedCollectibles> savedUnlockedCollectibles)
        {
            _savedQuestData = savedQuestData;
            _savedUnlockedCollectibles = savedUnlockedCollectibles;
        }

        public void SetResourceLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public void SetModel(QuestDataModel model)
        {
            _model = model;
        }

        #endregion

        public override IEnumerator Initialize()
        {
            if (_resourceLoader == null)
            {
                _resourceLoader = new ResourceLoader();
            }

            if (_savedQuestData == null)
            {
                _savedQuestData = new SaveSystem<SavedQuestData>(TagManager.FILENAME_SAVEDUNLOCKEDCOLLECTIBLES);
            }
            _model.SetCurrentQuestData(_savedQuestData.Load());

            if (_savedUnlockedCollectibles == null)
            {
                _savedUnlockedCollectibles = new SaveSystem<SavedUnlockedCollectibles>(TagManager.FILENAME_SAVEDUNLOCKEDCOLLECTIBLES);
            }
            _model.SetUnlockedCollectibles(_savedUnlockedCollectibles.Load());


            LoadQuestCollection();
            SetCollectibleCollectionAndUnlockedCollectible();

            yield return base.Initialize();
        }

        #region PRIVATE METHOD

        private void LoadQuestCollection()
        {
            SOQuestCollection gameConstants = _resourceLoader.Load<SOQuestCollection>(TagManager.SO_QUESTCOLLECTION);
            if (gameConstants != null)
            {
                _model.SetQuestCollection(gameConstants);
            }
            else
            {
                Debug.LogError("QUESTCOLLECTION SCRIPTABLE NOT FOUND!");
            }
        }

        private void SetCollectibleCollectionAndUnlockedCollectible()
        {
            SOCollectibleCollection collectibleCollection = _resourceLoader.Load<SOCollectibleCollection>(TagManager.SO_COLLECTIBLECOLLECTION);
            if (collectibleCollection == null)
            {
                Debug.LogError("COLLECTIBLECOLLECTION SCRIPTABLE NOT FOUND!");
                return;
            }

            if (collectibleCollection.CollectibleItems == null ||
                _model.UnlockedCollectibles == null ||
                _model.UnlockedCollectibles.Items == null)
            {
                return;
            }

            _model.SetCurrentCollectibleAmount(_model.UnlockedCollectibles.Items.Count);

            _model.SetCurrentPuzzleAmount(_model.GetCurrentCollectibleByTypeAmount(
                collectibleCollection.CollectibleItems,
                _model.UnlockedCollectibles.Items,
                EnumManager.CollectibleType.Puzzle));

            _model.SetCurrentHiddenObjectAmount(_model.GetCurrentCollectibleByTypeAmount(
                collectibleCollection.CollectibleItems,
                _model.UnlockedCollectibles.Items,
                EnumManager.CollectibleType.HiddenObject));
        }

        #endregion

        #region MESSAGE LISTENER

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

        #endregion
    }
}
