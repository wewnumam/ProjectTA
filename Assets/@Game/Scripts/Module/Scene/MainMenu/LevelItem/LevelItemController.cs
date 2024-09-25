using Agate.MVC.Base;
using ProjectTA.Message;


namespace ProjectTA.Module.LevelItem
{
    public class LevelItemController : ObjectController<LevelItemController, LevelItemView>
    {
        public void Init(LevelItemView view)
        {
            SetView(view);
        }

        public override void SetView(LevelItemView view)
        {
            base.SetView(view);
            view.SetCallback(OnChooseLevel);
        }

        private void OnChooseLevel()
        {
            Publish(new ChooseLevelMessage(_view.levelData));
        }
    }
}