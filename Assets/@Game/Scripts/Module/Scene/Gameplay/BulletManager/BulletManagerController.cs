using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameState;
using UnityEngine;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerController : ObjectController<BulletManagerController, BulletManagerView>
    {
        private float _shootingRate;
        private GameStateController gameState;

        public void SetShootingRate(float shootingRate) => _shootingRate = shootingRate;

        public void SetInitialActivateJoystick(bool isJoystickActive) => _view.isJoystickActive = isJoystickActive;

        public override void SetView(BulletManagerView view)
        {
            base.SetView(view);
            view.shootingRate = _shootingRate;
        }

        internal void OnActivateJoystick(ActivateJoystickMessage message)
        {
            _view.isJoystickActive = message.IsJoystickActive;
        }

        internal void OnShootStart(PlayerCharacterShootStartMessage message)
        {
            if (gameState.IsStatePlaying() && !_view.isJoystickActive)
                _view.StartShooting();
        }

        internal void OnShootEnd(PlayerCharacterShootEndMessage message)
        {
            _view.isShooting = false;
        }

        internal void OnAim(RotatePlayerCharacterMessage message)
        {
            if (message.Aim.sqrMagnitude > _view.joystickShootRange)
            {
                _view.StartShooting();
            }
            else
            {
                _view.isShooting = false;
            }
        }
    }
}