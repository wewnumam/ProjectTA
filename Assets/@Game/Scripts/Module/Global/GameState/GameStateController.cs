using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.GameState
{
    public class GameStateController : DataController<GameStateController, GameStateModel, IGameStateModel>
    {        
        public override IEnumerator Initialize()
        {
            _model.SetGameState(EnumManager.GameState.PreGame);
            Debug.Log($"STATE: {EnumManager.GameState.PreGame}");

            return base.Initialize();
        }

        public void SetGameState(GameStateMessage message)
        {
            _model.SetGameState(message.GameState);
            Debug.Log($"STATE: {message.GameState}");
        }
    }
}
