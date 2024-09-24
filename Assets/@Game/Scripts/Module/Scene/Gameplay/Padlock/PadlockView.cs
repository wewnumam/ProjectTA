using Agate.MVC.Base;
using ProjectTA.Module.PadlockItem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.Padlock
{
    public class PadlockView : BaseView
    {
        public Button closeButton;

        [Header("Padlock Item Properties")]
        public Canvas canvas;
        public Transform padlockItemTemplateParent;
        public PadlockItemView padlockItemTemplate;
        public Transform padlockItemPlaceTemplateParent;
        public PadlockItemPlace padlockItemPlaceTemplate;

        [Header("Callbacks")]
        public UnityEvent onShowPadlock;
        public UnityEvent onClosePadlock;

        public void SetCallback(UnityAction onClose)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(onClose);
        }
    }
}