using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticConnector : BaseConnector
    {
        private readonly AnalyticController _analytic = new();

        protected override void Connect()
        {
            Subscribe<AppQuitMessage>(_analytic.OnAppQuit);
        }

        protected override void Disconnect()
        {
            Unsubscribe<AppQuitMessage>(_analytic.OnAppQuit);
        }
    }
}