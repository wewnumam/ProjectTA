using Agate.MVC.Base;
using ProjectTA.Message;
using System;
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
            Publish(new AddKillCountMessage(1));
        }

        public void Init(EnemyView view)
        {
            SetView(view);
        }

    }
}