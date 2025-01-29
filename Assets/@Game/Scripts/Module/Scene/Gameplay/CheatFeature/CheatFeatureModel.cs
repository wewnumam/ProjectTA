using Agate.MVC.Base;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureModel : BaseModel
    {
        public bool IsJoystickActive { get; private set; } = false;
        public Transform PlayerCharacter { get; private set; } = null;

        private readonly List<Transform> _puzzleTransforms = new();
        private readonly List<Transform> _hiddenObjectTransforms = new();
        private int _currentPuzzleIndex = 0;
        private int _currentHiddenObjectIndex = 0;

        public void SetIsJoystickActive(bool isJoystickActive)
        {
            IsJoystickActive = isJoystickActive;
        }

        public void SetPlayerCharacter(Transform playerCharacter)
        {
            PlayerCharacter = playerCharacter;
        }

        public void InitCollectibleTransforms()
        {
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

        public void TeleportToPuzzle()
        {
            PlayerCharacter.position = _puzzleTransforms[_currentPuzzleIndex].position;
            _currentPuzzleIndex++;

            if (_currentPuzzleIndex >= _puzzleTransforms.Count)
                _currentPuzzleIndex = 0;
        }

        public void TeleportToHiddenObject()
        {
            PlayerCharacter.position = _hiddenObjectTransforms[_currentHiddenObjectIndex].position;
            _currentHiddenObjectIndex++;

            if (_currentHiddenObjectIndex >= _hiddenObjectTransforms.Count)
                _currentHiddenObjectIndex = 0;
        }

        
    }
}