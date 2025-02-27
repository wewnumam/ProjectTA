using Agate.MVC.Base;
using ProjectTA.Message;
using ProjectTA.Module.GameConstants;
using ProjectTA.Utility;
using System.Collections;
using UnityEngine;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticController : ObjectController<AnalyticController, AnalyticModel, AnalyticView>
    {
        #region UTILITY

        private IResourceLoader _resourceLoader = null;

        public void SetResourceLoader(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
        }

        public void SetModel(AnalyticModel model)
        {
            _model = model;
        }

        #endregion

        public override IEnumerator Initialize()
        {
            if (_resourceLoader == null)
            {
                _resourceLoader = new ResourceLoader();
            }

            Application.logMessageReceived += HandleLog;
            Application.quitting += HandleQuit;

            if (_view == null)
            {
                SetView(GetNewAnalyticView());
            }

            SOGameConstants gameConstants = _resourceLoader.Load<SOGameConstants>(TagManager.SO_GAMECONSTANTS);
            if (gameConstants != null)
            {
                _model.SetAnalyticFormConstants(gameConstants.AnalyticFormConstants);
                _view.SetFpsUpdateInterval(gameConstants.AnalyticFormConstants.FpsUpdateInterval);
            }
            else
            {
                Debug.LogError("GAMECONSTANT SCRIPTABLE NOT FOUND!");
            }

            yield return base.Initialize();
        }

        public override void SetView(AnalyticView view)
        {
            base.SetView(view);

            view.SetCallback(OnUpdatePerformanceMetrics);
        }

        public AnalyticView GetNewAnalyticView()
        {
            GameObject obj = GameObject.Instantiate(new GameObject());
            obj.name = nameof(AnalyticView);
            GameObject.DontDestroyOnLoad(obj);
            obj.AddComponent<AnalyticView>();
            return obj.GetComponent<AnalyticView>();
        }

        #region CALLBACKS

        public void OnUpdatePerformanceMetrics(PerformanceMetricsData performanceMetrics)
        {
            _model.SetPerformanceMetrics(performanceMetrics);
        }

        public void HandleQuit()
        {
            if (!_model.IsAppQuit)
            {
                OnAppQuit(new AppQuitMessage());
            }
        }

        public void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (_model.LogMessages.Contains(logString))
            {
                return;
            }
            _model.LogMessages.Add(logString);
            _model.AddLogCounter(type);
        }

        #endregion

        #region MESSAGE LISTENER

        public void OnAppQuit(AppQuitMessage message)
        {
            Application.logMessageReceived -= HandleLog;
            Application.quitting -= HandleQuit;

            _model.SetIsAppQuit(true);
            _model.SetScreenTimeInSeconds(Time.unscaledTime);

            Publish(new AnalyticRecordMessage(_model.GetAnalyticRecord()));

            Debug.Log($"APP QUIT {_model.ScreenTimeInSeconds}");
        }

        #endregion
    }
}