using Agate.MVC.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.CollectibleItem
{
    public class CollectibleItemView : BaseView
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _chooseButton;

        public TMP_Text Title => _title;
        public Button ChooseButton => _chooseButton;

        public void SetCallback(UnityAction onChooseCollectible)
        {
            _chooseButton.onClick.RemoveAllListeners();
            _chooseButton.onClick.AddListener(onChooseCollectible);
        }
    }
}