using Agate.MVC.Base;

namespace ProjectTA.Module.HUD
{
    public interface IHUDModel : IBaseModel
    {
        int InitialHealth { get; }
        int CurrentHealth { get; }
    }
}