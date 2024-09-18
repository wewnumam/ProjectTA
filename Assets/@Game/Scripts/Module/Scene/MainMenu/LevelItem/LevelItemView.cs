using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelItem
{
    public class LevelItemView : ObjectView<ILevelItemModel>
    {
        [ReadOnly] public SO_LevelData levelData;

        public TMP_Text title;
        public Button chooseButton;

        public void SetCallback(UnityAction onChooseLevel)
        {
            chooseButton.onClick.RemoveAllListeners();
            chooseButton.onClick.AddListener(onChooseLevel);
        }

        protected override void InitRenderModel(ILevelItemModel model)
        {
        }

        protected override void UpdateRenderModel(ILevelItemModel model)
        {
        }
    }
}