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
        public TMP_Text currentLevelText;

        private UnityAction onPlay;

        public void SetCallback(UnityAction onPlay)
        {
            this.onPlay = onPlay;
        }

        public void Play()
        {
            onPlay?.Invoke();
        }

        protected override void InitRenderModel(ILevelSelectionModel model)
        {
        }

        protected override void UpdateRenderModel(ILevelSelectionModel model)
        {
            if (model.CurrentLevelData != null)
                currentLevelText.SetText(model.CurrentLevelData.title);
        }

    }
}