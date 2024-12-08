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
        public override void SetView(PlayerCharacterView view)
        {
            base.SetView(view);
            view.SetCollideCallbacks(OnCollideWithEnemy, OnCollideWithCollectibleComponent, OnCollideWithPadlock);
            view.rb.isKinematic = false;
        }

        private void OnCollideWithEnemy()
        {
            Publish(new SubtractHealthMessage(1));
        }

        private void OnCollideWithCollectibleComponent(SO_CollectibleData collectibleData)
        {
            Publish(new UnlockCollectibleMessage(collectibleData));

            if (collectibleData.dialogue != null)
            {
                Publish(new ShowDialogueMessage(collectibleData.dialogue));
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
            _view.isJoystickActive = isJoystickActive;
        }

        internal void OnGameOver(GameOverMessage message)
        {
            _view.animator.Play(TagManager.ANIM_DEAD);
        }

        internal void OnGameWin(GameWinMessage message)
        {
        }

        internal void OnMove(MovePlayerCharacterMessage message)
        {
            _view.direction = message.Direction;

            if (message.Direction == Vector2.zero)
                _view.animator.Play(TagManager.ANIM_IDLE);
            else
                _view.animator.Play(TagManager.ANIM_WALK);

        }

        internal void OnRotate(RotatePlayerCharacterMessage message)
        {
            _view.aim = message.Aim;
        }

        internal void OnActivateJoystick(ActivateJoystickMessage message)
        {
            _view.isJoystickActive = message.IsJoystickActive;
        }

    }
}