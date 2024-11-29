using Agate.MVC.Base;

namespace ProjectTA.Module.Countdown
{
    public interface ICountdownModel : IBaseModel
    {
        float InitialCountdown { get; }
        float CurrentCountdown { get; }
    }
}