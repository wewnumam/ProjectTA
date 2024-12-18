using Agate.MVC.Base;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.GameConstants
{
    public class GameConstantsController : DataController<GameConstantsController, GameConstantsModel, IGameConstantsModel>
    {
        private string _fileName = "GameConstants";

        public void SetFileName(string fileName)
        {
            _fileName = fileName;
        }

        public void SetModel(GameConstantsModel model)
        {
            _model = model;
        }

        public override IEnumerator Initialize()
        {
            try
            {
                SOGameConstants gameConstants = Resources.Load<SOGameConstants>(_fileName);
                _model.SetGameConstants(gameConstants);
            }
            catch (Exception e)
            {
                Debug.LogError("GAMECONSTANT SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            yield return base.Initialize();
        }
    }
}
