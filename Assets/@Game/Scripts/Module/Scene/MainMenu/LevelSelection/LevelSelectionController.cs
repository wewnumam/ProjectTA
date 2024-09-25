using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelItem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionController : ObjectController<LevelSelectionController, LevelSelectionModel, ILevelSelectionModel, LevelSelectionView>
    {
        public void SetLevelCollection(SO_LevelCollection levelCollection) => _model.SetLevelCollection(levelCollection);
        public void SetCurrentLevelData(SO_LevelData levelData) => _model.SetCurrentLevelData(levelData);
        public void SetUnlockedLevels(List<string> unlockedLevels) => _model.SetUnlockedLevels(unlockedLevels);

        public override void SetView(LevelSelectionView view)
        {
            base.SetView(view);

            for (global::System.Int32 i = view.listedLevel.Count; i < _model.LevelCollection.levelItems.Count; i++)
            {
                GameObject obj = GameObject.Instantiate(view.template, view.container);
                obj.SetActive(true);

                LevelItemView levelItemView = obj.GetComponent<LevelItemView>();
                SO_LevelData levelItem = _model.LevelCollection.levelItems[i];

                view.listedLevel.Add(levelItemView);

                levelItemView.levelData = levelItem;
                levelItemView.title.SetText(levelItem.title);
                
                if (levelItem.isLockedLevel)
                    levelItemView.GetComponent<Button>().interactable = _model.IsLevelUnlocked(levelItem.levelGate.name);

                LevelItemController levelItemController = new LevelItemController();
                InjectDependencies(levelItemController);
                levelItemController.Init(levelItemView);
            }
        }

        internal void OnChooseLevel(ChooseLevelMessage message)
        {
            _model.SetCurrentLevelData(message.LevelData);
        }
    }
}