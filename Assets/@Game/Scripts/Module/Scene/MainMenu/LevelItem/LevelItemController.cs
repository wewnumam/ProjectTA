using Agate.MVC.Base;
using ProjectTA.Message;


namespace ProjectTA.Module.LevelItem
{
    public class LevelItemController : ObjectController<LevelItemController, LevelItemModel, ILevelItemModel, LevelItemView>
    {
        public void Init(LevelItemModel model, LevelItemView view)
        {
            _model = model;
            SetView(view);
        }

        public override void SetView(LevelItemView view)
        {
            base.SetView(view);
            view.SetCallback(OnChooseLevel);
        }

        private void OnChooseLevel()
        {
            Publish(new UnlockLevelMessage(_view.levelData));
            Publish(new ChooseLevelMessage(_view.levelData.name));
        }
    }
}