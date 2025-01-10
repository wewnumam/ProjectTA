using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.SpatialDirection
{
    public class SpatialDirectionConnector : BaseConnector
    {
        private readonly SpatialDirectionController _spatialDirection = new();

        protected override void Connect()
        {
            Subscribe<UpdatePuzzleCountMessage>(_spatialDirection.OnUpdatePuzzleCount);
            Subscribe<GameOverMessage>(_spatialDirection.OnGameOver);
            Subscribe<GameWinMessage>(_spatialDirection.OnGameWin);
        }

        protected override void Disconnect()
        {
            Unsubscribe<UpdatePuzzleCountMessage>(_spatialDirection.OnUpdatePuzzleCount);
            Unsubscribe<GameOverMessage>(_spatialDirection.OnGameOver);
            Unsubscribe<GameWinMessage>(_spatialDirection.OnGameWin);
        }
    }
}