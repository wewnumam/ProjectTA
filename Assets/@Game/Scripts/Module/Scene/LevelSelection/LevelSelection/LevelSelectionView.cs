using Agate.MVC.Base;
using Cinemachine;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionView : ObjectView<ILevelSelectionModel>
    {
        [Header("Level Data")]
        [ReadOnly] public SO_LevelCollection LevelCollection;
        public List<Transform> ListedModels;

        [Header("UI References")]
        public TMP_Text CurrentLevelTitle;
        public TMP_Text CurrentLevelDescription;
        public Button playButton;
        public CinemachineVirtualCamera VirtualCamera;

        private UnityAction _onPlay;
        private UnityAction _onMainMenu;
        private UnityAction _onNext;
        private UnityAction _onPrevious;

        public void Next()
        {
            _onNext?.Invoke();
        }

        public void Previous()
        {
            _onPrevious?.Invoke();
        }

        public void SetCallbacks(UnityAction onPlay, UnityAction onMainMenu, UnityAction onNext, UnityAction onPrevious)
        {
            _onPlay = onPlay;
            _onMainMenu = onMainMenu;
            _onNext = onNext;
            _onPrevious = onPrevious;
        }

        public void Play() => _onPlay?.Invoke();
        public void MainMenu() => _onMainMenu?.Invoke();

        protected override void InitRenderModel(ILevelSelectionModel model) { }

        protected override void UpdateRenderModel(ILevelSelectionModel model)
        {
            if (model.CurrentLevelData != null)
            {
                CurrentLevelTitle.SetText(model.CurrentLevelData.title);
                CurrentLevelDescription.SetText(model.CurrentLevelData.description);
            }
        }
    }
}
