using Agate.MVC.Base;

namespace ProjectTA.Module.SaveSystem
{
    public interface ISaveSystemModel : IBaseModel
    {
        public SaveData SaveData { get; }
    }
}