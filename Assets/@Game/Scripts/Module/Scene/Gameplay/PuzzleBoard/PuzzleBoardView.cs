﻿using Agate.MVC.Base;
using System.Collections.Generic;
using UnityEngine.Events;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleBoardView : BaseView
    {
        public List<Puzzle> puzzles;
        public UnityEvent onShow, onComplete;
    }

    [System.Serializable]
    public class Puzzle
    {
        public string label;
        public PuzzleComponent puzzleComponent;
        public PuzzleDragable puzzleDragable;
    }
}