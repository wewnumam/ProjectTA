using Agate.MVC.Base;
using ProjectTA.Message;

namespace ProjectTA.Module.BugReport
{
    public class BugReportController : ObjectController<BugReportController, BugReportView>
    {
        public override void SetView(BugReportView view)
        {
            base.SetView(view);
            view.SetCallback(SendReport);
        }

        private void SendReport(string reportMessage)
        {
            Publish(new ReportMessage(reportMessage, _view.SetResponse));
        }
    }
}