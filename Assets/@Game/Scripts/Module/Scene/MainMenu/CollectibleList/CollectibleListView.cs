using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.CollectibleItem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectTA.Module.CollectibleList
{
    public class CollectibleListView : BaseView
    {
        public Transform parent;
        public CollectibleItemView itemTemplate;
        public TMP_Text descriptionText;

        [ReadOnly]
        public List<CollectibleItemView> collectibleItems;
    }
}