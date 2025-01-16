using Agate.MVC.Base;
using ProjectTA.Message;
using UnityEngine;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticController : DataController<AnalyticController, AnalyticModel>
    {
        public void OnAppQuit(AppQuitMessage message)
        {
            _model.SetScreenTimeInSeconds(Time.unscaledTime);
        }
    }
}