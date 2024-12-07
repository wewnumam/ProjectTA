using Agate.MVC.Base;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.Tutorial
{
    public class TutorialView : BaseView
    {
        public Image image;
        public TMP_Text title;
        public TMP_Text description;

        public List<TutorialItem> tutorialItems;

        private UnityAction _onNext, _onPrevious;

        public void SetCallbacks(UnityAction onNext, UnityAction onPrevious)
        {
            _onNext = onNext;
            _onPrevious = onPrevious;
        }

        public void Next()
        {
            _onNext?.Invoke();
        }

        public void Previous()
        {
            _onPrevious?.Invoke();
        }
    }

    [Serializable]
    public class TutorialItem
    {
        [ShowAssetPreview]
        public Sprite image;
        public string title;
        [TextArea]
        public string description;
    }
}