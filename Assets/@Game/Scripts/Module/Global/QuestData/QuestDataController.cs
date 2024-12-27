using Agate.MVC.Base;
using ProjectTA.Message;
using System;
using System.Collections;
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
            Publish(new UpdateQuestDataMessage(_model.CurrentQuestData));
        }

        public void OnGameWin(GameWinMessage message)
        {
            Publish(new UpdateQuestDataMessage(_model.CurrentQuestData));
        }

        internal void OnAddKillCount(AddKillCountMessage message)
        {
            _model.AddCurrentKillAmount(message.Amount);
        }
    }
}
