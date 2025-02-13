using Agate.MVC.Base;
using ProjectTA.Module.CollectibleItem;
using TMPro;
using UnityEngine;

namespace ProjectTA.Module.CollectibleList
{
    public class CollectibleListView : BaseView
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private CollectibleItemView _itemTemplate;
        [SerializeField] private TMP_Text _descriptionText;
        [field: SerializeField]
        public TMP_Text CollectionCountText { get; set; }

        public Transform Parent => _parent;
        public CollectibleItemView ItemTemplate => _itemTemplate;
        public TMP_Text DescriptionText => _descriptionText;
    }
}