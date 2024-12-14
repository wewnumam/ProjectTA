using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureController : ObjectController<CheatFeatureController, CheatFeatureView>
    {
        private List<Transform> _puzzleTransforms = new();
        private List<Transform> _hiddenObjectTransforms = new();
        private int _currentPuzzleIndex;
        private int _currentHiddenObjectIndex;

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
            _view.activateJoystickToggle.isOn = isJoystickActive;
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
            Publish(new AddHealthMessage(1));
        }

        private void OnSubtractHealth()
        {
            Publish(new SubtractHealthMessage(1));
        }

        private void OnAddPuzzlePieceCount()
        {
            Publish(new AddCollectedPuzzlePieceCountMessage(1));
        }

        private void OnSubtractPuzzlePieceCount()
        {
            Publish(new SubtractCollectedPuzzlePieceCountMessage(1));
        }

        private void OnAddHiddenObjectCount()
        {
            Publish(new AddCollectedHiddenObjectCountMessage(1));
        }

        private void OnSubtractHiddenObjectCount()
        {
            Publish(new SubtractCollectedPuzzlePieceCountMessage(1));
        }

        private void OnAddKillCount()
        {
            Publish(new AddKillCountMessage(1));
        }

        private void OnSubtractKillCount()
        {
            Publish(new SubtractKillCountMessage(1));
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
    }
}