using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.SaveSystem;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterModel, PlayerCharacterView>
    {
        public void InitModel(IGameConstantsModel gameConstants, IGameSettingsModel gameSettings)
        {
            if (!ValidateGameConstants(gameConstants) || !ValidateGameSettings(gameSettings))
                return;

            _model.SetIsJoystickActive(gameConstants.GameConstants.IsJoystickActive);
            _model.SetPlayerConstants(gameConstants.GameConstants.PlayerConstants);
            _model.SetIsVibrationOn(gameSettings.SavedSettingsData.IsVibrationOn);
        }

        public override void SetView(PlayerCharacterView view)
        {
            base.SetView(view);
            view.SetCollideCallbacks(OnCollideWithEnemy, OnCollideWithCollectibleComponent, OnCollideWithPadlock);
            view.DisableFreezePositionY();
            view.PlayerConstants = _model.PlayerConstants;
            view.SetIsJoyStickActive(_model.IsJoystickActive);
        }

        #region PRIVATE METHOD

        private bool ValidateGameConstants(IGameConstantsModel gameConstants)
        {
            if (gameConstants == null)
                return LogError("GAMECONSTANTS IS NULL");

            if (gameConstants.GameConstants == null)
                return LogError("SOGAMECONSTANTS IS NULL");

            if (gameConstants.GameConstants.PlayerConstants == null)
                return LogError("PLAYERCONSTANTS IS NULL");

            return true;
        }

        private bool ValidateGameSettings(IGameSettingsModel gameSettings)
        {
            if (gameSettings == null)
                return LogError("GAMESETTINGS IS NULL");

            if (gameSettings.SavedSettingsData == null)
                return LogError("SAVEDSETTINGSDATA IS NULL");

            return true;
        }

        private bool LogError(string message)
        {
            Debug.LogError(message);
            return false;
        }

        #endregion

        #region CALLBACK LISTENER

        private void OnCollideWithEnemy()
        {
            Publish(new AdjustHealthCountMessage(-1));

            #if UNITY_ANDROID
            if (_model.IsVibrationOn)
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
                Publish(new AdjustCollectedPuzzlePieceCountMessage(1));
            }
            else if (collectibleData.Type == EnumManager.CollectibleType.HiddenObject)
            {
                Publish(new AdjustCollectedHiddenObjectCountMessage(1));
            }
        }

        private void OnCollideWithPadlock()
        {
            Publish(new ShowPadlockMessage());
        }

        #endregion

        #region MESSAGE LISTENER

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
            _model.SetIsJoystickActive(message.IsJoystickActive);
            _view.SetIsJoyStickActive(message.IsJoystickActive);
        }

        public void OnVibrate(ToggleVibrationMessage message)
        {
            _model.SetIsVibrationOn(message.Vibration);
        }

        #endregion
    }
}