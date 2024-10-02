using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameState;
using System;
using UnityEngine;

namespace ProjectTA.Module.BulletManager
{
    public class BulletManagerController : ObjectController<BulletManagerController, BulletManagerView>
    {
        private float _shootingRate;
        private GameStateController gameState;

        public void SetShootingRate(float shootingRate) => _shootingRate = shootingRate;

        public override void SetView(BulletManagerView view)
        {
            base.SetView(view);
            view.shootingRate = _shootingRate;
        }

        internal void OnShootStart(PlayerCharacterShootStartMessage message)
        {
            if (gameState.IsStatePlaying())
                _view.StartShoot();
        }

        internal void OnShootEnd(PlayerCharacterShootEndMessage message)
        {
            _view.isShoot = false;
        }
    }
}