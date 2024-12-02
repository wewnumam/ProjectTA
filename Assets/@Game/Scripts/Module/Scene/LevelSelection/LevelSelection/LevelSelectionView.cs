using Agate.MVC.Base;
using DG.Tweening;
using NaughtyAttributes;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectTA.Module.LevelSelection
{
    public class LevelSelectionView : ObjectView<ILevelSelectionModel>
    {
        [Header("Level Data")]
        [ReadOnly] public SO_LevelCollection LevelCollection;
        [ReadOnly] public List<LevelItemView> ListedLevels;

        [Header("UI References")]
        public TMP_Text CurrentLevelTitle;
        public TMP_Text CurrentLevelDescription;

        [Header("Carousel Settings")]
        public RectTransform[] CarouselItems;
        public RectTransform FrontPosition;
        public RectTransform LeftPosition;
        public RectTransform RightPosition;
        public RectTransform BackPosition;
        public Transform Parent;

        [Header("Navigation Buttons")]
        public Button NextButton;
        public Button PreviousButton;

        [Header("Animation Settings")]
        public float AnimationDuration = 0.5f;

        private UnityAction _onPlay;
        private UnityAction _onMainMenu;
        private UnityAction _onNext;
        private UnityAction _onPrevious;

        private Queue<RectTransform> _itemQueue;

        private void Start()
        {
            InitializeCarousel();
            AssignButtonListeners();
        }

        private void InitializeCarousel()
        {
            _itemQueue = new Queue<RectTransform>(CarouselItems);
            UpdateCarousel();
        }

        private void AssignButtonListeners()
        {
            NextButton.onClick.AddListener(MoveNext);
            PreviousButton.onClick.AddListener(MovePrevious);
        }

        private void UpdateCarousel(bool isMoveNext = false)
        {
            var itemsArray = _itemQueue.ToArray();
            SetItemState(itemsArray[0], FrontPosition, Parent.childCount - 1, true); // Front
            SetItemState(itemsArray[1], RightPosition, isMoveNext ? Parent.childCount - 4 : Parent.childCount - 2, true); // Right
            SetItemState(itemsArray[2], BackPosition, Parent.childCount - 3, true);  // Back
            SetItemState(itemsArray[3], LeftPosition, isMoveNext ? Parent.childCount - 2 : Parent.childCount - 4, true);  // Left

            HideExtraItems();
        }

        private void HideExtraItems()
        {
            for (int i = 4; i < CarouselItems.Length; i++)
            {
                SetItemState(CarouselItems[i], BackPosition, 0, false);
            }
        }

        private void SetItemState(RectTransform item, RectTransform targetPosition, int siblingIndex, bool isVisible)
        {
            item.position = targetPosition.position;
            item.SetSiblingIndex(siblingIndex);
            item.gameObject.SetActive(isVisible);
        }

        private void MoveNext()
        {
            _onNext?.Invoke();
            RotateQueueForward();
            AnimateCarousel(true);
            UpdateCarousel(true);
        }

        private void MovePrevious()
        {
            _onPrevious?.Invoke();
            RotateQueueBackward();
            AnimateCarousel();
            UpdateCarousel();
        }

        private void RotateQueueForward()
        {
            var movedItem = _itemQueue.Last();
            _itemQueue = new Queue<RectTransform>(new[] { movedItem }.Concat(_itemQueue.Take(_itemQueue.Count - 1)));
        }

        private void RotateQueueBackward()
        {
            var movedItem = _itemQueue.Dequeue();
            _itemQueue.Enqueue(movedItem);
        }

        private void AnimateCarousel(bool isMoveNext = false)
        {
            var itemsArray = _itemQueue.ToArray();
            AnimateItemToPosition(itemsArray[isMoveNext ? 0 : 2], RightPosition.position);
            AnimateItemToPosition(itemsArray[isMoveNext ? 1 : 3], BackPosition.position);
            AnimateItemToPosition(itemsArray[isMoveNext ? 2 : 0], LeftPosition.position);
            AnimateItemToPosition(itemsArray[isMoveNext ? 3 : 1], FrontPosition.position);
        }

        private void AnimateItemToPosition(RectTransform item, Vector3 targetPosition)
        {
            item.DOMove(targetPosition, AnimationDuration).SetEase(Ease.InOutQuad);
        }

        public void SetCallbacks(UnityAction onPlay, UnityAction onMainMenu, UnityAction onNext, UnityAction onPrevious)
        {
            _onPlay = onPlay;
            _onMainMenu = onMainMenu;
            _onNext = onNext;
            _onPrevious = onPrevious;
        }

        public void Play() => _onPlay?.Invoke();
        public void MainMenu() => _onMainMenu?.Invoke();

        protected override void InitRenderModel(ILevelSelectionModel model) { }

        protected override void UpdateRenderModel(ILevelSelectionModel model)
        {
            if (model.CurrentLevelData != null)
            {
                CurrentLevelTitle.SetText(model.CurrentLevelData.title);
                CurrentLevelDescription.SetText(model.CurrentLevelData.description);
            }
        }
    }
}
