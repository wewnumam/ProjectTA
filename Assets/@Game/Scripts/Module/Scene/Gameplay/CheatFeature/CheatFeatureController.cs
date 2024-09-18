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
            Publish(new AddHealthMessage(_view.healthAmount));
        }

        private void OnSubtractHealth()
        {
            Publish(new SubtractHealthMessage(_view.healthAmount));
        }
    }
}