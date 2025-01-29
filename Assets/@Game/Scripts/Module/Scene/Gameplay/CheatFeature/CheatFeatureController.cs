using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureController : ObjectController<CheatFeatureController, CheatFeatureModel, CheatFeatureView>
    {
        public void InitModel(IGameConstantsModel gameConstants)
        {
            if (gameConstants == null)
            {
                Debug.LogError("GAMECONSTANTS IS NULL");
                return;
            }

            if (gameConstants.GameConstants == null)
            {
                Debug.LogError("SOGAMECONSTANTS IS NULL"); 
                return;
            }

            _model.SetIsJoystickActive(gameConstants.GameConstants.IsJoystickActive);
        }

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
            view.SetEnvironmentCallbacks(OnTeleportToPuzzle, OnTeleportToHiddenObject);
            view.SetEffectsCallbacks(OnBlurCamera, OnNormalCamera);
            view.SetCountdownCallbacks(OnRestartCountdown, OnResetCountdown);
            
            view.ActivateJoystickToggle.isOn = _model.IsJoystickActive;

            _model.SetPlayerCharacter(GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER).transform);
            _model.InitCollectibleTransforms();
        }

        #region CALLBACK LISTENER

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
            Publish(new AdjustHealthCountMessage(1));
        }

        private void OnSubtractHealth()
        {
            Publish(new AdjustHealthCountMessage(-1));
        }

        private void OnAddPuzzlePieceCount()
        {
            Publish(new AdjustCollectedPuzzlePieceCountMessage(1));
        }

        private void OnSubtractPuzzlePieceCount()
        {
            Publish(new AdjustCollectedPuzzlePieceCountMessage(-1));
        }

        private void OnAddHiddenObjectCount()
        {
            Publish(new AdjustCollectedHiddenObjectCountMessage(1));
        }

        private void OnSubtractHiddenObjectCount()
        {
            Publish(new AdjustCollectedHiddenObjectCountMessage(-1));
        }

        private void OnAddKillCount()
        {
            Publish(new AdjustKillCountMessage(1));
        }

        private void OnSubtractKillCount()
        {
            Publish(new AdjustKillCountMessage(-1));
        }

        private void OnTeleportToPuzzle()
        {
            _model.TeleportToPuzzle();
        }

        private void OnTeleportToHiddenObject()
        {
            _model.TeleportToHiddenObject();
        }

        private void OnBlurCamera()
        {
            Publish(new CameraBlurMessage());
        }

        private void OnNormalCamera()
        {
            Publish(new CameraNormalMessage());
        }

        private void OnRestartCountdown()
        {
            Publish(new CountdownRestartMessage());
        }

        private void OnResetCountdown()
        {
            Publish(new CountdownResetMessage());
        }

        #endregion
    }
}