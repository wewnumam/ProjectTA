using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.Enemy
{
    public class EnemyController : ObjectController<EnemyController, EnemyView>
    {
        public override void SetView(EnemyView view)
        {
            base.SetView(view);
            view.SetCallback(OnKill);
        }

        private void OnKill()
        {
            Publish(new AdjustKillCountMessage(1));
        }

        public void Init(EnemyView view, Transform player, float destroyDelay)
        {
            SetView(view);
            view.SetPlayer(player);
            view.SetDestroyDelay(destroyDelay);
        }

    }
}