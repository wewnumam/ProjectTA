using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.HUD
{
    public class HudController : ObjectController<HudController, HudView>
    {
        public void SetGateIcon(Sprite sprite) => _view.GateIcon.sprite = sprite;

        public void OnUpdateHealth(UpdateHealthMessage message)
        {
            _view.HealthSlider.maxValue = message.InitialHealth;
            _view.HealthSlider.value = message.CurrentHealth;
        }

        public void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            _view.PuzzleCountText.SetText(message.PuzzlePieceCount.ToString());
            for (int i = 0; i < message.PuzzlePieceCount; i++)
            {
                if (message.PuzzlePieceCount < _view.PuzzleBarItems.Count)
                {
                    _view.PuzzleBarItems[i].gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < message.CollectedPuzzlePieceCount; i++)
            {
                if (message.CollectedPuzzlePieceCount < _view.PuzzleBarItems.Count)
                {
                    _view.PuzzleBarItems[i].color = _view.CollectedPuzzleBarColor;
                }
            }
        }

        public void OnUpdateKillCount(UpdateKillCountMessage message)
        {
            _view.KillCountText.SetText($"{message.KillCount}");
        }

        public void OnUpdateCountdown(UpdateCountdownMessage message)
        {
            _view.TimerText.SetText(message.GetFormattedCurrentCountdown());
        }

        public void OnUpdateHiddenObjectCount(UpdateHiddenObjectCountMessage message)
        {
            _view.HiddenObjectCount.SetText($"{message.CollectedHiddenObjectCount}/{message.HiddenObjectCount}");
        }
    }
}