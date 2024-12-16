using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.CollectibleData;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CollectibleItem
{
    public class CollectibleItemView : BaseView
    {
        [ReadOnly] public SOCollectibleData CollectibleData;

        public TMP_Text title;
        public Button chooseButton;

        public void SetCallback(UnityAction onChooseCollectible)
        {
            chooseButton.onClick.RemoveAllListeners();
            chooseButton.onClick.AddListener(onChooseCollectible);
        }
    }
}