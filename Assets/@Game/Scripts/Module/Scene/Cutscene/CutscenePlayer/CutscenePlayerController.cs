using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerController : ObjectController<CutscenePlayerController, CutscenePlayerModel, ICutscenePlayerModel , CutscenePlayerView>
    {
        public void SetModel(CutscenePlayerModel model)
        {
            _model = model;
        }

        public void SetDialogueAsset(TextAsset textAsset) => _model.InitStory(textAsset);

        public void InitEnvironment(CutsceneComponent component) => _model.SetCameras(component.Cameras);

        public override void SetView(CutscenePlayerView view)
        {
            base.SetView(view);

            view.SetCallback(DisplayNextLine);
            DisplayNextLine();
        }

        public void DisplayNextLine()
        {
            if (!_model.Story.canContinue)
            {
                SceneLoader.Instance.LoadScene(TagManager.SCENE_LEVELSELECTION);
                return;
            }

            if (_model.IsTextAnimationComplete)
            {   
                _model.SetNextLine();
                _model.UpdateDialogueLine();
                _model.SetIsTextAnimationComplete(false);
                _model.GoNextCamera();
            }
            else
            {
                _model.UpdateDialogueLine();
                _model.SetIsTextAnimationComplete(true);
            }
        }
    }
}