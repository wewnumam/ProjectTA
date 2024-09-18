using Agate.MVC.Base;

namespace ProjectTA.Module.Health
{
    public interface IHealthModel : IBaseModel
    {
        int InitialHealth { get; }
        int CurrentHealth { get; }
    }
}