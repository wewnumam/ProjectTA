using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;
using UnityEngine;

namespace ProjectTA.Module.HUD
{
    public class HUDController : ObjectController<HUDController, HUDView>
    {
        private float _initialCountdown, _timeRemaining, _minutes, _seconds;

        public void SetInitialCountdown(float initialCountdown)
        {
            _initialCountdown = initialCountdown;
        }

        public void SetGateIcon(Sprite sprite) => _view.gateIcon.sprite = sprite;

        internal void OnUpdateHealth(UpdateHealthMessage message)
        {
            _view.healthSlider.maxValue = message.InitialHealth;
            _view.healthSlider.value = message.CurrentHealth;
        }

        internal void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            _view.puzzleCountText.SetText(message.PuzzlePieceCount.ToString());
            for (int i = 0; i < message.PuzzlePieceCount; i++)
            {
                if (message.PuzzlePieceCount < _view.puzzleBarItems.Count)
                {
                    _view.puzzleBarItems[i].gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < message.CollectedPuzzlePieceCount; i++)
            {
                if (message.CollectedPuzzlePieceCount < _view.puzzleBarItems.Count)
                {
                    _view.puzzleBarItems[i].color = _view.collectedPuzzleBarColor;
                }
            }
        }

        internal void OnUpdatePuzzleSolvedCount(UpdatePuzzleSolvedCountMessage message)
        {
        }

        internal void OnUpdateKillCount(UpdateKillCountMessage message)
        {
            _view.killCountText.SetText($"{message.KillCount}");
        }

        internal void OnUpdateCountdown(UpdateCountdownMessage message)
        {
            _view.timerText.SetText(message.GetFormattedCurrentCountdown());
        }

        internal void OnUpdateHiddenObjectCount(UpdateHiddenObjectCountMessage message)
        {
            _view.hiddenObjectCount.SetText($"{message.CollectedHiddenObjectCount}/{message.HiddenObjectCount}");
        }
    }
}