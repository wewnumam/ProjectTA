using Agate.MVC.Base;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.Tutorial
{
    public class TutorialView : BaseView
    {
        [SerializeField] private Image _imageComponent;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private List<TutorialItem> _tutorialItems;
        private UnityAction _onNext, _onPrevious;

        public Image ImageComponent => _imageComponent;
        public TMP_Text TitleText => _titleText;
        public TMP_Text DescriptionText => _descriptionText;
        public List<TutorialItem> TutorialItems => _tutorialItems;

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
}