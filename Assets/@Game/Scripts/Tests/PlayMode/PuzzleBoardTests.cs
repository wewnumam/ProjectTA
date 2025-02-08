using Agate.MVC.Core;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.PuzzleBoard;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class PuzzleBoardTests
    {
        private PuzzleBoardController _controller;
        private PuzzleBoardModel _model;
        private PuzzleBoardView _view;
        private TestLevelDataModel _testLevelData;

        [SetUp]
        public void Setup()
        {
            // Initialize controller and model
            _controller = new PuzzleBoardController();
            _model = new PuzzleBoardModel();
            _controller.SetModel(_model);

            // Create test level data
            var puzzleObjects = new List<PuzzleObject>
            {
                new PuzzleObject(ScriptableObject.CreateInstance<SOCollectibleData>(), Vector2.zero, Vector2.zero)
            };

            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            levelData.PuzzleQuestion = "Test Question";
            levelData.PuzzleObjects = puzzleObjects;

            _testLevelData = new TestLevelDataModel
            {
                CurrentLevelData = levelData
            };
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_view?.gameObject);
        }

        #region Model Tests

        [Test]
        public void SetPuzzleObjects_SetsCorrectly()
        {
            // Arrange
            var puzzleObjects = new List<PuzzleObject>
            {
                new PuzzleObject(ScriptableObject.CreateInstance<SOCollectibleData>(), Vector2.zero, Vector2.zero)
            };

            // Act
            _model.SetPuzzleObjects(puzzleObjects);

            // Assert
            Assert.AreEqual(puzzleObjects, _model.PuzzleObjects);
        }

        [Test]
        public void SetPuzzleQuestion_SetsCorrectly()
        {
            // Arrange
            const string question = "Test Question";

            // Act
            _model.SetPuzzleQuestion(question);

            // Assert
            Assert.AreEqual(question, _model.PuzzleQuestion);
        }

        [Test]
        public void InitObjects_ValidatesParameters()
        {
            // Act & Assert
            LogAssert.Expect(LogType.Error, "InitObjects: One or more parameters are null.");
            _model.InitObjects(null, null, null, null);
        }

        [Test]
        public void InitObjects_ValidatesPuzzleObjects()
        {
            // Arrange
            _model.SetPuzzleObjects(null);

            // Act & Assert
            LogAssert.Expect(LogType.Warning, "InitObjects: PuzzleObjects list is null or empty.");
            _model.InitObjects(new GameObject(), new GameObject(), new GameObject().transform, _controller.OnPuzzlePlaced);
        }

        #endregion

        #region Controller Tests

        [Test]
        public void InitModel_ValidData_SetsModelProperties()
        {
            // Act
            _controller.InitModel(_testLevelData);

            // Assert
            Assert.AreEqual(_testLevelData.CurrentLevelData.PuzzleQuestion, _model.PuzzleQuestion);
            Assert.AreEqual(_testLevelData.CurrentLevelData.PuzzleObjects, _model.PuzzleObjects);
        }

        [Test]
        public void InitModel_NullLevelData_LogsError()
        {
            // Act & Assert
            LogAssert.Expect(LogType.Error, "LEVELDATA IS NULL");
            _controller.InitModel(null);
        }

        [Test]
        public void InitModel_NullCurrentLevelData_LogsError()
        {
            // Arrange
            var levelData = new TestLevelDataModel { CurrentLevelData = null };

            // Act & Assert
            LogAssert.Expect(LogType.Error, "CURRENTLEVELDATA IS NULL");
            _controller.InitModel(levelData);
        }

        [Test]
        public void InitModel_NullPuzzleObjects_LogsError()
        {
            // Arrange
            var levelData = new TestLevelDataModel
            {
                CurrentLevelData = ScriptableObject.CreateInstance<SOLevelData>()
            };

            // Act & Assert
            LogAssert.Expect(LogType.Error, "PUZZLEOBJECTS IS NULL");
            _controller.InitModel(levelData);
        }

        [Test]
        public void SetView_InitializesCallbacksAndModel()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            _view = GetView();
            LogAssert.Expect(LogType.Error, "InitObjects: One or more parameters are null.");

            // Act
            _controller.SetView(_view);

            // Assert
            Assert.AreEqual(_model.PuzzleQuestion, _view.QuestionText.text);
        }

        [Test]
        public void OnPuzzlePlaced_PublishesMessage()
        {
            // Arrange
            var puzzleDragable = new GameObject().AddComponent<PuzzleDragable>();

            // Act
            _controller.OnPuzzlePlaced(puzzleDragable);

            // Assert
            LogAssert.Expect(LogType.Log, puzzleDragable.gameObject.name);
        }

        [Test]
        public void OnClose_ResetsTimeScale()
        {
            // Arrange
            Time.timeScale = 0;

            // Act
            _controller.OnClose();

            // Assert
            Assert.AreEqual(1, Time.timeScale);
        }

        [Test]
        public void ShowPuzzleBoard_StopsTime()
        {
            // Arrange
            _view = GetView();
            _controller.SetView(_view);
            Time.timeScale = 1;
            LogAssert.Expect(LogType.Error, "InitObjects: One or more parameters are null.");

            // Act
            _controller.ShowPuzzleBoard(new ShowPadlockMessage());

            // Assert
            Assert.AreEqual(0, Time.timeScale);
        }

        #endregion

        #region PuzzleDragable Tests

        [Test]
        public void PuzzleDragable_SetActive_InvokesOnActive()
        {
            // Arrange
            var dragable = new GameObject().AddComponent<PuzzleDragable>();
            bool onActiveCalled = false;
            dragable.onActive.AddListener(() => onActiveCalled = true);

            // Act
            dragable.SetPuzzleDragableActive();

            // Assert
            Assert.IsTrue(onActiveCalled);
            Assert.IsTrue(dragable.isActive);
        }

        [Test]
        public void PuzzleDragable_OnEndDrag_SnapsToTarget()
        {
            // Arrange
            var dragable = new GameObject().AddComponent<PuzzleDragable>();
            var target = new GameObject().AddComponent<RectTransform>();
            dragable.targetPosition = target;
            dragable.DraggableRect = dragable.gameObject.AddComponent<RectTransform>();
            dragable.Canvas = new GameObject().AddComponent<Canvas>();
            dragable.CanvasGroup = dragable.gameObject.AddComponent<CanvasGroup>();
            dragable.InitialAnchoredPosition = Vector2.zero;

            // Set target bounds
            target.anchoredPosition = Vector2.zero;
            target.sizeDelta = new Vector2(100, 100);

            // Simulate drag within bounds
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(50, 50)
            };

            // Act
            dragable.OnBeginDrag(eventData);
            dragable.OnDrag(eventData);
            dragable.OnEndDrag(eventData);

            // Assert
            Assert.AreEqual(target.anchoredPosition, dragable.DraggableRect.anchoredPosition);
            Assert.IsFalse(dragable.isActive);
        }

        [Test]
        public void PuzzleDragable_OnEndDrag_ResetsPosition_WhenOutOfBounds()
        {
            // Arrange
            var dragable = new GameObject().AddComponent<PuzzleDragable>();
            var target = new GameObject().AddComponent<RectTransform>();
            dragable.targetPosition = target;
            dragable.DraggableRect = dragable.gameObject.AddComponent<RectTransform>();
            dragable.Canvas = new GameObject().AddComponent<Canvas>();
            dragable.CanvasGroup = dragable.gameObject.AddComponent<CanvasGroup>();
            dragable.InitialAnchoredPosition = Vector2.zero;

            // Set target bounds
            target.anchoredPosition = Vector2.zero;
            target.sizeDelta = new Vector2(100, 100);

            // Simulate drag outside bounds
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(200, 200)
            };

            // Act
            dragable.OnBeginDrag(eventData);
            dragable.OnDrag(eventData);
            dragable.OnEndDrag(eventData);

            // Assert
            Assert.AreEqual(Vector2.zero, dragable.DraggableRect.anchoredPosition);
            Assert.IsTrue(dragable.isActive);
        }

        #endregion

        #region Helper Classes

        private class TestLevelDataModel : ILevelDataModel
        {
            public SOLevelData CurrentLevelData { get; set; }

            public SOCutsceneData CurrentCutsceneData => throw new System.NotImplementedException();

            public SOLevelCollection LevelCollection => throw new System.NotImplementedException();

            public SavedLevelData SavedLevelData => throw new System.NotImplementedException();

            public GameObject CurrentEnvironmentPrefab => throw new System.NotImplementedException();

            public event OnDataModified OnDataModified;

            public List<SOLevelData> GetUnlockedLevels()
            {
                throw new System.NotImplementedException();
            }

            public bool IsMemberValid()
            {
                return true;
            }
        }

        private PuzzleBoardView GetView()
        {
            PuzzleBoardView view = _controller.GetNewPuzzleBoardView();
            view.Parent = view.gameObject.transform;
            view.PuzzleDragableTemplate = view.gameObject.AddComponent<PuzzleDragable>();
            view.PuzzleTargetTemplate = view.gameObject.AddComponent<RectTransform>();
            view.QuestionText = view.gameObject.AddComponent<TextMeshProUGUI>();
            return view;
        }

        #endregion
    }
}