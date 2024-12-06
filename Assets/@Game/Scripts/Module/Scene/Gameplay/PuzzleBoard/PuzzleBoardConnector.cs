using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardConnector : BaseConnector
    {
        private PuzzleBoardController _puzzleBoard;

        protected override void Connect()
        {
            Subscribe<ShowPadlockMessage>(_puzzleBoard.ShowPuzzleBoard);
            Subscribe<TeleportToPuzzleMessage>(_puzzleBoard.TeleportToPuzzle);
            Subscribe<GameWinMessage>(_puzzleBoard.OnGameWin);
            Subscribe<UnlockCollectibleMessage>(_puzzleBoard.OnUnlockCollectible);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ShowPadlockMessage>(_puzzleBoard.ShowPuzzleBoard);
            Unsubscribe<TeleportToPuzzleMessage>(_puzzleBoard.TeleportToPuzzle);
            Unsubscribe<GameWinMessage>(_puzzleBoard.OnGameWin);
            Unsubscribe<UnlockCollectibleMessage>(_puzzleBoard.OnUnlockCollectible);
        }
    }
}