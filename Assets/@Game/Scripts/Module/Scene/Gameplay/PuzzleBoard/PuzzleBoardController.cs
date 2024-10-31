using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System;
using UnityEngine;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardController : ObjectController<PuzzleBoardController, PuzzleBoardView>
    {
        int currentPuzzleIndex;

        public override void SetView(PuzzleBoardView view)
        {
            base.SetView(view);
            foreach (var puzzle in view.puzzles)
            {
                puzzle.puzzleComponent.onSendPuzzleComponent += OnPuzzleComponentSended;
                puzzle.puzzleDragable.onPlace += OnPuzzlePlaced;
            }
            
            Debug.Log($"<color=green>[{view.GetType()}]</color> installed successfully!");
        }

        private void OnPuzzleComponentSended(PuzzleComponent puzzleComponent)
        {
            Puzzle puzzle = _view.puzzles.Find(p => p.puzzleComponent == puzzleComponent);
            puzzle.puzzleDragable.SetPuzzleDragableActive();

            Debug.Log(puzzleComponent.gameObject.name);

            Publish(new AddCollectedPuzzlePieceCountMessage(1));
        }

        private void OnPuzzlePlaced(PuzzleDragable puzzleDragable)
        {
            Publish(new AddPadlockOnPlaceMessage(1));

            Debug.Log(puzzleDragable.gameObject.name);
        }

        internal void ShowPuzzleBoard(ShowPadlockMessage message)
        {
            _view.onShow?.Invoke();
        }

        internal void TeleportToPuzzle(TeleportToPuzzleMessage message)
        {
            GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER).transform.position = _view.puzzles[currentPuzzleIndex].puzzleComponent.transform.position;
            
            currentPuzzleIndex++;
            
            if (currentPuzzleIndex >= _view.puzzles.Count)
                currentPuzzleIndex = 0;
        }

        internal void OnGameWin(GameWinMessage message)
        {
            _view.onComplete?.Invoke();
        }
    }
}