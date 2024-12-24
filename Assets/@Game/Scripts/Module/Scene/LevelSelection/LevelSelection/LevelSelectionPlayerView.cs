using Agate.MVC.Base;
using Cinemachine;
using NaughtyAttributes;
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
        [SerializeField] private Image _imageIcon;
        [SerializeField] private Button _playButton;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private UnityEvent _onLock;
        [SerializeField] private UnityEvent _onUnlock;
        [SerializeField, ReadOnly, ResizableTextArea] string _log;

        private UnityAction _onPlay, _onMainMenu, _onNext, _onPrevious;

        public void Next()
        {
            _onNext?.Invoke();
        }

        public void Previous()
        {
            _onPrevious?.Invoke();
        }

        public void Play()
        {
            _onPlay?.Invoke();
        }

        public void MainMenu()
        {
            _onMainMenu?.Invoke();
        }

        public void SetCallbacks(UnityAction onPlay, UnityAction onMainMenu, UnityAction onNext, UnityAction onPrevious)
        {
            _onPlay = onPlay;
            _onMainMenu = onMainMenu;
            _onNext = onNext;
            _onPrevious = onPrevious;
        }

        protected override void InitRenderModel(ILevelSelectionPlayerModel model) { }

        protected override void UpdateRenderModel(ILevelSelectionPlayerModel model)
        {
            if (model.CurrentLevelData != null)
            {
                _currentLevelTitle.SetText(model.CurrentLevelData.Title);
                _currentLevelDescription.SetText(model.CurrentLevelData.Description);
                _imageIcon.sprite = model.CurrentLevelData.Icon;
                _virtualCamera.LookAt = _listedModels[model.CurrentLevelDataIndex];
                _virtualCamera.Follow = _listedModels[model.CurrentLevelDataIndex];
                _playButton.interactable = model.IsCurrentLevelUnlocked();
                if (model.IsCurrentLevelUnlocked())
                {
                    _onUnlock?.Invoke();
                }
                else
                {
                    _onLock?.Invoke();
                }
            }

            _log = model.GetLog();
        }
    }
}
