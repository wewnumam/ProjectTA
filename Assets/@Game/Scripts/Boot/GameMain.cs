using Agate.MVC.Base;
using Agate.MVC.Core;
using ProjectTA.Module.Analytic;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.GameConstants;
using ProjectTA.Module.GoogleFormUploader;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.QuestData;
using ProjectTA.Module.SaveSystem;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Boot
{
    public class GameMain : BaseMain<GameMain>, IMain
    {
        protected override IController[] GetDependencies()
        {
            return new IController[] {
                new GameSettingsController(),
                new LevelDataController(),
                new CollectibleDataController(),
                new GameConstantsController(),
                new QuestDataController(),
                new GoogleFormUploaderController(),
                new AnalyticController(),
            };
        }

        protected override IConnector[] GetConnectors()
        {
            return new IConnector[] {
                new GameSettingsConnector(),
                new LevelDataConnector(),
                new CollectibleDataConnector(),
                new QuestDataConnector(),
                new GoogleFormUploaderConnector(),
                new AnalyticConnector(),
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
