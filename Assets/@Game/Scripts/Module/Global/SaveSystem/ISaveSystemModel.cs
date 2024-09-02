using Agate.MVC.Base;
using ProjectTA.Utility;

namespace ProjectTA.Module.SaveSystem
{
    public interface ISaveSystemModel : IBaseModel
    {
        public SaveData SaveData { get; }
    }
}