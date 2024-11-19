using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerView : ObjectView<ICutscenePlayerModel>
    {
        [ReadOnly] public SO_CutsceneData cutsceneData;
        public TMP_Text characterText;
        public Text messageText;
        public Image image;
        
        public UnityAction onNext;

        public void DisplayNextLine()
        {
            onNext?.Invoke();
        }

        public void SetCallback(UnityAction onNext)
        {
            this.onNext = onNext;
        }

        protected override void InitRenderModel(ICutscenePlayerModel model)
        {
        }

        protected override void UpdateRenderModel(ICutscenePlayerModel model)
        {
        }
    }
}