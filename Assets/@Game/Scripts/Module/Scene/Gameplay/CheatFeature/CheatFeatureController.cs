using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureController : ObjectController<CheatFeatureController, CheatFeatureView>
    {
        private readonly List<Transform> _puzzleTransforms = new();
        private readonly List<Transform> _hiddenObjectTransforms = new();
        private int _currentPuzzleIndex = 0;
        private int _currentHiddenObjectIndex = 0;

        public override void SetView(CheatFeatureView view)
        {
            base.SetView(view);
            view.SetUtilityCallbacks(OnDeleteSaveData, OnActivateJoystick);
            view.SetGameStateCallbacks(OnGameWin, OnGameOver);
            view.SetHealthCallbacks(OnAddHealth, OnSubtractHealth);
            view.SetMissionCallbacks(
                OnAddPuzzlePieceCount,
                OnSubtractPuzzlePieceCount,
                OnAddHiddenObjectCount,
                OnSubtractHiddenObjectCount,
                OnAddKillCount,
                OnSubtractKillCount);
            view.SetEnvironmentCallbacks(OnTeleportToPuzzle, OnTeleportToCollectible);
            view.SetEffectsCallbacks(OnBlurCamera, OnNormalCamera);
            view.SetCountdownCallbacks(OnRestartCountdown, OnResetCountdown);

            foreach (var collectibleObj in GameObject.FindGameObjectsWithTag(TagManager.TAG_COLLECTIBLE))
            {
                if (collectibleObj.TryGetComponent<CollectibleComponent>(out var collectibleComponent))
                {
                    if (collectibleComponent.CollectibleData.Type == EnumManager.CollectibleType.Puzzle)
                    {
                        _puzzleTransforms.Add(collectibleObj.transform);
                    }
                    else if (collectibleComponent.CollectibleData.Type == EnumManager.CollectibleType.HiddenObject)
                    {
                        _hiddenObjectTransforms.Add(collectibleObj.transform);
                    }
                }
            }
        }

        public void SetInitialActivateJoystick(bool isJoystickActive)
        {
            _view.ActivateJoystickToggle.isOn = isJoystickActive;
        }

        private void OnDeleteSaveData()
        {
            Publish(new DeleteSaveDataMessage());
        }

        private void OnActivateJoystick(bool isJoysticActive)
        {
            Publish(new ActivateJoystickMessage(isJoysticActive));
        }

        private void OnGameWin()
        {
            Publish(new GameWinMessage());
        }

        private void OnGameOver()
        {
            Publish(new GameOverMessage());
        }

        private void OnAddHealth()
        {
            Publish(new AdjustHealthCountMessage(1));
        }

        private void OnSubtractHealth()
        {
            Publish(new AdjustHealthCountMessage(-1));
        }

        private void OnAddPuzzlePieceCount()
        {
            Publish(new AdjustCollectedPuzzlePieceCountMessage(1));
        }

        private void OnSubtractPuzzlePieceCount()
        {
            Publish(new AdjustCollectedPuzzlePieceCountMessage(-1));
        }

        private void OnAddHiddenObjectCount()
        {
            Publish(new AdjustCollectedHiddenObjectCountMessage(1));
        }

        private void OnSubtractHiddenObjectCount()
        {
            Publish(new AdjustCollectedHiddenObjectCountMessage(-1));
        }

        private void OnAddKillCount()
        {
            Publish(new AdjustKillCountMessage(1));
        }

        private void OnSubtractKillCount()
        {
            Publish(new AdjustKillCountMessage(-1));
        }

        private void OnTeleportToPuzzle()
        {
            GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER).transform.position = _puzzleTransforms[_currentPuzzleIndex].position;
            _currentPuzzleIndex++;

            if (_currentPuzzleIndex >= _puzzleTransforms.Count)
                _currentPuzzleIndex = 0;
        }

        private void OnTeleportToCollectible()
        {
            GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER).transform.position = _hiddenObjectTransforms[_currentHiddenObjectIndex].position;
            _currentHiddenObjectIndex++;

            if (_currentHiddenObjectIndex >= _hiddenObjectTransforms.Count)
                _currentHiddenObjectIndex = 0;
        }

        private void OnBlurCamera()
        {
            Publish(new CameraBlurMessage());
        }

        private void OnNormalCamera()
        {
            Publish(new CameraNormalMessage());
        }

        private void OnRestartCountdown()
        {
            Publish(new CountdownRestartMessage());
        }

        private void OnResetCountdown()
        {
            Publish(new CountdownResetMessage());
        }
    }
}