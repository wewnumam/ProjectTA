using Agate.MVC.Base;
using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionPlayerView : ObjectView<ILevelSelectionPlayerModel>
    {
        [Header("Level Data")]
        [SerializeField] private List<Transform> _listedModels;

        [Header("UI References")]
        [SerializeField] private TMP_Text _currentLevelTitle;
        [SerializeField] private TMP_Text _currentLevelDescription;
        [SerializeField] private Button _playButton;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public List<Transform> ListedModels => _listedModels;
        public TMP_Text CurrentLevelTitle => _currentLevelTitle;
        public TMP_Text CurrentLevelDescription => _currentLevelDescription;
        public Button PlayButton => _playButton;
        public CinemachineVirtualCamera VirtualCamera => _virtualCamera;

        private UnityAction _onPlay, _onMainMenu, _onNext, _onPrevious;

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

        protected override void InitRenderModel(ILevelSelectionPlayerModel model) { }

        protected override void UpdateRenderModel(ILevelSelectionPlayerModel model)
        {
            if (model.CurrentLevelData != null)
            {
                CurrentLevelTitle.SetText(model.CurrentLevelData.Title);
                CurrentLevelDescription.SetText(model.CurrentLevelData.Description);
            }
        }
    }
}
