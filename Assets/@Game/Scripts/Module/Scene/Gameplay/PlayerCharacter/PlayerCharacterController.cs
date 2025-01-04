using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using System;
using UnityEngine;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterView>
    {
        private bool _isVibrationOn = false;

        public void SetInitialVibration(bool isVibrationOn)
        {
            _isVibrationOn = isVibrationOn;
        }

        public override void SetView(PlayerCharacterView view)
        {
            base.SetView(view);
            view.SetCollideCallbacks(OnCollideWithEnemy, OnCollideWithCollectibleComponent, OnCollideWithPadlock);
            view.DisableKinematic();
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

        public void OnVibrate(GameSettingVibrateMessage message)
        {
            _isVibrationOn = message.Vibrate;
        }
    }
}