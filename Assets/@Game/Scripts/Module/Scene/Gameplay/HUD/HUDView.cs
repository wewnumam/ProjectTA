using Agate.MVC.Base;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.HUD
{
    public class HudView : BaseView
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TMP_Text _puzzleCountText;
        [SerializeField] private TMP_Text _killCountText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _hiddenObjectCount;
        [SerializeField] private List<Image> _puzzleBarItems;
        [SerializeField] private Color _collectedPuzzleBarColor;
        [SerializeField] private Image _gateIcon;

        public Slider HealthSlider => _healthSlider;
        public TMP_Text PuzzleCountText => _puzzleCountText;
        public TMP_Text KillCountText => _killCountText;
        public TMP_Text TimerText => _timerText;
        public TMP_Text HiddenObjectCount => _hiddenObjectCount;
        public List<Image> PuzzleBarItems => _puzzleBarItems;
        public Color CollectedPuzzleBarColor => _collectedPuzzleBarColor;
        public Image GateIcon => _gateIcon;
    }
}