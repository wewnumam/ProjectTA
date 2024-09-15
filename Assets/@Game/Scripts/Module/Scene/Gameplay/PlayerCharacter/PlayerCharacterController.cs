using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterController : ObjectController<PlayerCharacterController, PlayerCharacterView>
    {
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
    }
}