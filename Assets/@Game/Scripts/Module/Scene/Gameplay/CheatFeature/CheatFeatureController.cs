using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.CheatFeature
{
    public class CheatFeatureController : ObjectController<CheatFeatureController, CheatFeatureView>
    {
        public override void SetView(CheatFeatureView view)
        {
            base.SetView(view);
            view.SetActivateJoystickCallbacks(OnActivateJoystick);
            view.SetGameStateCallbacks(OnGameWin, OnGameOver);
            view.SetHealthCallbacks(OnAddHealth, OnSubtractHealth);
            view.SetMissionCallbacks(OnAddPuzzlePieceCount, OnSubtractPuzzlePieceCount, OnAddKillCount, OnSubtractKillCount);
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
            Publish(new AddHealthMessage(1));
        }

        private void OnSubtractHealth()
        {
            Publish(new SubtractHealthMessage(1));
        }

        private void OnAddPuzzlePieceCount()
        {
            Publish(new AddCollectedPuzzlePieceCountMessage(1));
        }

        private void OnSubtractPuzzlePieceCount()
        {
            Publish(new SubtractCollectedPuzzlePieceCountMessage(1));
        }

        private void OnAddKillCount()
        {
            Publish(new AddKillCountMessage(1));
        }

        private void OnSubtractKillCount()
        {
            Publish(new SubtractKillCountMessage(1));
        }
    }
}