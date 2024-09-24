using Agate.MVC.Base;
using ProjectTA.Message;


namespace ProjectTA.Module.PadlockItem
{
    public class PadlockItemController : ObjectController<PadlockItemController, PadlockItemView>
    {
        public void Init(PadlockItemView view)
        {
            SetView(view);
        }

        public override void SetView(PadlockItemView view)
        {
            base.SetView(view);
            view.SetCallback(OnPlace);
        }

        private void OnPlace()
        {
            Publish(new AddPadlockOnPlaceMessage(1));
        }
    }
}