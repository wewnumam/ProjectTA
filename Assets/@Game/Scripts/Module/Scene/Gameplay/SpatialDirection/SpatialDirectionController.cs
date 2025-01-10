using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.SpatialDirection
{
    public class SpatialDirectionController : ObjectController<SpatialDirectionController, SpatialDirectionView>
    {
        private Transform _target = null;

        public override void SetView(SpatialDirectionView view)
        {
            base.SetView(view);

            GameObject targetObj = GameObject.FindGameObjectWithTag(TagManager.TAG_PADLOCK);

            if (targetObj != null)
            {
                _target = targetObj.transform;
            }
            else
            {
                Debug.LogError($"GAMEOBJECT WITH TAG {TagManager.TAG_PADLOCK} NOT FOUND!");
            }

            view.Target = _target;
            _view.IsActive.Invoke(false);
        }

        public void OnUpdatePuzzleCount(UpdatePuzzleCountMessage message)
        {
            if (message.CollectedPuzzlePieceCount >= message.PuzzlePieceCount)
            {
                _view.IsActive.Invoke(true);
            }
        }

        public void OnGameOver(GameOverMessage message)
        {
            DeactivateSpatialDirection();
        }

        public void OnGameWin(GameWinMessage message)
        {
            DeactivateSpatialDirection();
        }

        private void DeactivateSpatialDirection()
        {
            _view.Target = null;
            _view.IsActive.Invoke(false);
        }
    }
}