using Agate.MVC.Base;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.GameConstants
{
    public class GameConstantsController : DataController<GameConstantsController, GameConstantsModel, IGameConstantsModel>
    {
        public override IEnumerator Initialize()
        {
            SOGameConstants gameConstants = Resources.Load<SOGameConstants>(@"GameConstants");
            _model.SetGameConstants(gameConstants);

            return base.Initialize();
        }
    }
}
