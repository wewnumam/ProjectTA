using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardConnector : BaseConnector
    {
        private readonly PuzzleBoardController _puzzleBoard = new();

        protected override void Connect()
        {
            Subscribe<ShowPadlockMessage>(_puzzleBoard.ShowPuzzleBoard);
            Subscribe<GameWinMessage>(_puzzleBoard.OnGameWin);
            Subscribe<UnlockCollectibleMessage>(_puzzleBoard.OnUnlockCollectible);
        }

        protected override void Disconnect()
        {
            Unsubscribe<ShowPadlockMessage>(_puzzleBoard.ShowPuzzleBoard);
            Unsubscribe<GameWinMessage>(_puzzleBoard.OnGameWin);
            Unsubscribe<UnlockCollectibleMessage>(_puzzleBoard.OnUnlockCollectible);
        }
    }
}