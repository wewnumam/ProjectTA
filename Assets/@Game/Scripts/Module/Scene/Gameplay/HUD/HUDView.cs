using Agate.MVC.Base;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectTA.Module.HUD
{
    public class HUDView : BaseView
    {
        public Slider healthSlider;
        public TMP_Text puzzleCountText;
        public TMP_Text killCountText;
        public TMP_Text timerText;
        public List<Image> puzzleBarItems;
        public Color collectedPuzzleBarColor;
        public Image gateIcon;
    }
}