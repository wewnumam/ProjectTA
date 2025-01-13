using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.GameInduction
{
    public class GameInductionController : ObjectController<GameInductionController, GameInductionView>
    {
        private bool _isGameInductionActive = false;

        public void SetIsGameInductionActive(bool isActive)
        {
            _isGameInductionActive = isActive;
        }

        public override void SetView(GameInductionView view)
        {
            base.SetView(view);
            view.SetCallback(OnCloseGameInduction);
            if (_isGameInductionActive)
            {
                view.StartGameInduction();
                Publish(new ToggleGameInductionMessage(false));
            }
        }

        private void OnCloseGameInduction()
        {
            if (_isGameInductionActive)
            {
                Time.timeScale = 1;
            }
        }
    }
}