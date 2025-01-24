using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Module.LevelData;
using ProjectTA.Utility;
using UnityEngine;

namespace ProjectTA.Module.CutscenePlayer
{
    public class CutscenePlayerController : ObjectController<CutscenePlayerController, CutscenePlayerModel, ICutscenePlayerModel, CutscenePlayerView>
    {
        public void SetModel(CutscenePlayerModel model)
        {
            _model = model;
        }

        public void Init(SOCutsceneData currentCutsceneData)
        {

            if (currentCutsceneData == null)
            {
                Debug.LogError("SOCUTSCENEDATA IS NULL");
                return;
            }

            if (currentCutsceneData.DialogueAsset == null)
            {
                Debug.LogError("DIALOGUE ASSET IS NULL");
                return;
            }

            _model.InitStory(currentCutsceneData.DialogueAsset);

            if (currentCutsceneData.Environment == null)
            {
                Debug.LogError("ENVIRONMENT PREFAB IS NULL");
                return;
            }

            GameObject environment = GameObject.Instantiate(currentCutsceneData.Environment.gameObject);

            if (environment.TryGetComponent<CutsceneComponent>(out var cutsceneComponent))
            {
                _model.SetCameras(cutsceneComponent.Cameras);
            }
            else
            {
                Debug.LogWarning("ENVIRONMENT PREFAB HAS NO CUTSCENE COMPONENT");
            }
        }

        public override void SetView(CutscenePlayerView view)
        {
            base.SetView(view);

            view.SetCallback(DisplayNextLine);
            DisplayNextLine();
        }

        public void DisplayNextLine()
        {
            if (_model.Story == null)
            {
                Debug.LogError("STORY IS NULL");
                return;
            }

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