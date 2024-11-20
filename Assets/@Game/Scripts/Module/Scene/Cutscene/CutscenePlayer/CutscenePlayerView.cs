using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerView : ObjectView<ICutscenePlayerModel>
    {
        [ReadOnly] public SO_CutsceneData cutsceneData;
        public TMP_Text characterText;
        public Text messageText;
        public Transform imageParent;
        public Image imageTemplate;
        public Transform camera;
        public float distance = 200;
        
        public UnityAction onNext;

        public void DisplayNextLine()
        {
            onNext?.Invoke();
            camera.DOMoveZ(camera.position.z + distance, 1f);
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