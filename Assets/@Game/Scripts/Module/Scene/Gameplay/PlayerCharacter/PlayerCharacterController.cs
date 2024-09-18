using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterView>
    {
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