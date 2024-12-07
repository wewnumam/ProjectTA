using Agate.MVC.Base;
using System;

namespace ProjectTA.Module.Tutorial
{
    public class TutorialController : ObjectController<TutorialController, TutorialView>
    {
        private int _currentIndex;

        public override void SetView(TutorialView view)
        {
            base.SetView(view);

            view.SetCallbacks(OnNext, OnPrevious);
            UpdateContent();
        }

        private void OnNext()
        {
            _currentIndex++;
            _currentIndex = _currentIndex >= _view.tutorialItems.Count ? 0 : _currentIndex;
            UpdateContent();
        }

        private void OnPrevious()
        {
            _currentIndex--;
            _currentIndex = _currentIndex < 0 ? _view.tutorialItems.Count - 1 : _currentIndex;
            UpdateContent();
        }

        private void UpdateContent()
        {
            _view.image.sprite = _view.tutorialItems[_currentIndex].image;
            _view.title.SetText(_view.tutorialItems[_currentIndex].title);
            _view.description.SetText(_view.tutorialItems[_currentIndex].description);
        }
    }
}