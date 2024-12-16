using Agate.MVC.Base;
using System.Collections.Generic;

namespace ProjectTA.Module.CollectibleData
{
    public interface ICollectibleDataModel : IBaseModel
    {
        SOCollectibleCollection CollectibleCollection { get; }
        List<SOCollectibleData> UnlockedCollectibleItems { get; }
    }
}