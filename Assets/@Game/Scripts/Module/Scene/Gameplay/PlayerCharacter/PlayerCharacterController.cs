using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterView>
    {
        public override void SetView(PlayerCharacterView view)
        {
            base.SetView(view);
            view.SetCollideCallbacks(OnCollideWithEnemy, OnCollideWithPuzzlePiece, OnCollideWithPadlock);
        }

        private void OnCollideWithEnemy()
        {
            Publish(new SubtractHealthMessage(1));
        }

        private void OnCollideWithPuzzlePiece()
        {
            Publish(new AddCollectedPuzzlePieceCountMessage(1));
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
        }

        internal void OnGameWin(GameWinMessage message)
        {
        }

        internal void OnMove(MovePlayerCharacterMessage message)
        {
            _view.direction = message.Direction;
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