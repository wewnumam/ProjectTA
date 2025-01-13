using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.GameState;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.QuestData;
using ProjectTA.Module.SaveSystem;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Boot
{
    public class GameMain : BaseMain<GameMain>, IMain
    {
        protected override IConnector[] GetConnectors()
        {
            return new IConnector[] {
                new SaveSystemConnector(),
                new LevelDataConnector(),
                new CollectibleDataConnector(),
                new GameStateConnector(),
                new QuestDataConnector(),
            };
        }

        protected override IController[] GetDependencies()
        {
            return new IController[] {
                new SaveSystemController(),
                new LevelDataController(),
                new CollectibleDataController(),
                new GameConstantsController(),
                new GameStateController(),
                new QuestDataController(),
            };
        }

        protected override IEnumerator StartInit()
        {
            Application.targetFrameRate = -1;
            yield return null;
        }

        public void RunCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}
