using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelItem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionView : ObjectView<ILevelSelectionModel>
    {
        [ReadOnly] public SO_LevelCollection levelCollection;
        [ReadOnly] public List<LevelItemView> listedLevel;
        public Transform container;
        public GameObject template;
        public TMP_Text currentLevelTitle;
        public TMP_Text currentLevelDescription;

        private UnityAction onPlay, onMainMenu;

        public void SetCallback(UnityAction onPlay, UnityAction onMainMenu)
        {
            this.onPlay = onPlay;
            this.onMainMenu = onMainMenu;
        }

        public void Play()
        {
            onPlay?.Invoke();
        }

        public void MainMenu()
        {
            onMainMenu?.Invoke();
        }

        protected override void InitRenderModel(ILevelSelectionModel model)
        {
        }

        protected override void UpdateRenderModel(ILevelSelectionModel model)
        {
            if (model.CurrentLevelData != null)
            {
                currentLevelTitle.SetText(model.CurrentLevelData.title);
                currentLevelDescription.SetText(model.CurrentLevelData.description);
            }
        }

    }
}