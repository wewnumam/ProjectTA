using Agate.MVC.Base;
using ProjectTA.Boot;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticController : ObjectController<AnalyticController, AnalyticModel, AnalyticView>
    {
        public override IEnumerator Initialize()
        {
            Application.logMessageReceived += HandleLog;
            Application.quitting += HandleQuit;

            GameMain.Instance.gameObject.AddComponent<AnalyticView>();
            SetView(GameMain.Instance.gameObject.GetComponent<AnalyticView>());

            try
            {
                SOGameConstants gameConstants = Resources.Load<SOGameConstants>(@"GameConstants");
                _model.SetAnalyticFormConstants(gameConstants.AnalyticFormConstants);
                _view.SetFpsUpdateInterval(gameConstants.AnalyticFormConstants.FpsUpdateInterval);
            }
            catch (Exception e)
            {
                Debug.LogError("GAMECONSTANT SCRIPTABLE NOT FOUND!");
                Debug.LogException(e);
            }

            yield return base.Initialize();
        }

        public override void SetView(AnalyticView view)
        {
            base.SetView(view);

            view.SetCallback(OnUpdatePerformanceMetrics);
        }

        private void OnUpdatePerformanceMetrics(PerformanceMetricsData performanceMetrics)
        {
            _model.SetPerformanceMetrics(performanceMetrics);
        }

        private void HandleQuit()
        {
            if (!_model.IsAppQuit)
            {
                OnAppQuit(new AppQuitMessage());
            }
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            _model.AddLogCounter(type);
        }

        public void OnAppQuit(AppQuitMessage message)
        {
            Application.logMessageReceived -= HandleLog;
            Application.quitting -= HandleQuit;

            _model.SetIsAppQuit(true);
            _model.SetScreenTimeInSeconds(Time.unscaledTime);

            Publish(new AnalyticRecordMessage(_model.GetAnalyticRecord()));

            Debug.Log($"APP QUIT {_model.ScreenTimeInSeconds}");
        }
    }
}