using Agate.MVC.Base;
using System.Collections.Generic;

namespace ProjectTA.Module.CollectibleData
{
    public interface ICollectibleDataModel : IBaseModel
    {
        SO_CollectibleCollection CollectibleCollection { get; }
        List<SO_CollectibleData> UnlockedCollectibleItems { get; }
    }
}