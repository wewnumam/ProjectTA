using Agate.MVC.Base;
using ProjectTA.Message;
using System;

namespace ProjectTA.Module.Bullet
{
    public class BulletController : ObjectController<BulletController, BulletView>
    {
        public void Init(BulletView view, float destroyDelay)
        {
            SetView(view);
            view.SetDestroyDelay(destroyDelay);
        }

        public override void SetView(BulletView view)
        {
            base.SetView(view);
            view.SetCallback(OnCollideWithCollider);
        }

        private void OnCollideWithCollider()
        {
            Publish(new DespawnBulletMessage(_view.gameObject));
        }
    }
}