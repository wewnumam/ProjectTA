using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using UnityEngine;

namespace ProjectTA.Module.HUD
{
    public class HudController : ObjectController<HudController, HudModel, HudView>
    {
        public void InitModel(ILevelDataModel levelData)
        {
            if (levelData == null)
            {
                Debug.LogError("LEVELDATA IS NULL");
                return;
            }

            if (levelData.CurrentLevelData == null)
            {
                Debug.LogError("CURRENTLEVELDATA IS NULL");
                return;
            }

            if (levelData.CurrentLevelData.Icon == null)
            {
                Debug.LogError("CURRENTLEVELDATAICON IS NULL");
                return;
            }

            _model.SetGateIcon(levelData.CurrentLevelData.Icon);
        }


        public override void SetView(HudView view)
        {
            base.SetView(view);
            view.GateIcon.sprite = _model.GateIcon;
        }

        public void OnUpdateHealth(UpdateHealthMessage message)
        {
            _view.HealthSlider.maxValue = message.InitialHealth;
            _view.HealthSlider.value = message.CurrentHealth;
        }

        public void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            if (_view.PuzzleCountText == null || _view.PuzzleBarItems.Count <= 0)
            {
                return;
            }

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