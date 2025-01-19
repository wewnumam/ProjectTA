using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterView>
    {
        private PlayerConstants _playerConstants = null;
        private bool _isVibrationOn = false;

        public void SetPlayerConstants(PlayerConstants playerConstants)
        {
            _playerConstants = playerConstants;
        }

        public void SetInitialVibration(bool isVibrationOn)
        {
            _isVibrationOn = isVibrationOn;
        }

        public override void SetView(PlayerCharacterView view)
        {
            base.SetView(view);
            view.SetCollideCallbacks(OnCollideWithEnemy, OnCollideWithCollectibleComponent, OnCollideWithPadlock);
            view.DisableFreezePositionY();
            view.PlayerConstants = _playerConstants;
        }

        private void OnCollideWithEnemy()
        {
            Publish(new SubtractHealthMessage(1));
#if UNITY_ANDROID
            if (_isVibrationOn)
            {
                Handheld.Vibrate();
            }
#endif
        }

        private void OnCollideWithCollectibleComponent(SOCollectibleData collectibleData)
        {
            Publish(new UnlockCollectibleMessage(collectibleData));

            if (collectibleData.Dialogue != null)
            {
                Publish(new ShowDialogueMessage(collectibleData.Dialogue));
            }

            if (collectibleData.Type == EnumManager.CollectibleType.Puzzle)
            {
                Publish(new AddCollectedPuzzlePieceCountMessage(1));
            }
            else if (collectibleData.Type == EnumManager.CollectibleType.HiddenObject)
            {
                Publish(new AddCollectedHiddenObjectCountMessage(1));
            }
        }

        private void OnCollideWithPadlock()
        {
            Publish(new ShowPadlockMessage());
        }

        public void SetInitialActivateJoystick(bool isJoystickActive)
        {
            _view.SetIsJoyStickActive(isJoystickActive);
        }

        public void OnGameOver(GameOverMessage message)
        {
            _view.Animator.Play(TagManager.ANIM_DEAD);
        }

        public void OnMove(MovePlayerCharacterMessage message)
        {
            _view.SetDirection(message.Direction);

            if (message.Direction == Vector2.zero)
                _view.Animator.Play(TagManager.ANIM_IDLE);
            else
                _view.Animator.Play(TagManager.ANIM_WALK);

        }

        public void OnRotate(RotatePlayerCharacterMessage message)
        {
            _view.SetAim(message.Aim);
        }

        public void OnActivateJoystick(ActivateJoystickMessage message)
        {
            _view.SetIsJoyStickActive(message.IsJoystickActive);
        }

        public void OnVibrate(ToggleVibrationMessage message)
        {
            _isVibrationOn = message.Vibration;
        }
    }
}