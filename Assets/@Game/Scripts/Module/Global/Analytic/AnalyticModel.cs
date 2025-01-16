using Agate.MVC.Base;

namespace ProjectTA.Module.Analytic
{
    public class AnalyticModel : BaseModel
    {
        public float ScreenTimeInSeconds { get; private set; } = 0;

        public void SetScreenTimeInSeconds(float screenTimeInSeconds)
        {
            ScreenTimeInSeconds = screenTimeInSeconds;
        }
    }
}