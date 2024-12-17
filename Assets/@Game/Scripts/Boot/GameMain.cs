using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.GameSettings;
using ProjectTA.Module.GameState;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.SaveSystem;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Boot
{
    public class GameMain : BaseMain<GameMain>, IMain
    {
        private readonly SaveSystemController _saveSystem;
        private readonly LevelDataController _levelData;
        private readonly CollectibleDataController _collectibleData;

        protected override IConnector[] GetConnectors()
        {
            return new IConnector[] {
                new SaveSystemConnector(),
                new LevelDataConnector(),
                new CollectibleDataConnector(),
                new GameStateConnector(),
                new GameSettingsConnector(),
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
                new GameSettingsController(),
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
