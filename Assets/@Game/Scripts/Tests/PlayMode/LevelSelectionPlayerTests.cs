using Agate.MVC.Core;
using Cinemachine;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelSelection;
using ProjectTA.Utility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class LevelSelectionPlayerTests
    {
        private LevelSelectionPlayerController _controller;
        private LevelSelectionPlayerModel _model;
        private LevelSelectionPlayerView _view;
        private SOLevelCollection _testLevelCollection;
        private List<SOLevelData> _testUnlockedLevels;
        private TestLevelDataModel _testLevelData;

        [SetUp]
        public void Setup()
        {
            // Create test level data
            var level1 = ScriptableObject.CreateInstance<SOLevelData>();
            level1.name = "Level1";
            var level2 = ScriptableObject.CreateInstance<SOLevelData>();
            level2.name = "Level2";
            var cutscene = ScriptableObject.CreateInstance<SOCutsceneData>();

            _testLevelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();
            _testLevelCollection.LevelItems = new List<SOLevelData> { level1, level2 };

            _testUnlockedLevels = new List<SOLevelData> { level1 };

            // Create test level data model
            _testLevelData = new TestLevelDataModel
            {
                LevelCollection = _testLevelCollection,
                UnlockedLevels = _testUnlockedLevels,
                CurrentLevelData = level1,
                CurrentCutsceneData = cutscene
            };

            // Initialize controller and model
            _controller = new LevelSelectionPlayerController();
            _model = new LevelSelectionPlayerModel();
            _controller.SetModel(_model);

            // Create and setup view
            _view = _controller.GetNewQuizPlayerView();
            TestUtility.SetPrivateField(_view, "_listedModels", CreateTestModels());
            TestUtility.SetPrivateField(_view, "_currentLevelTitle", new GameObject().AddComponent<TextMeshProUGUI>());
            TestUtility.SetPrivateField(_view, "_currentLevelDescription", new GameObject().AddComponent<TextMeshProUGUI>());
            TestUtility.SetPrivateField(_view, "_imageIcon", new GameObject().AddComponent<Image>());
            TestUtility.SetPrivateField(_view, "_playButton", new GameObject().AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_virtualCamera", new GameObject().AddComponent<CinemachineVirtualCamera>());
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_view.gameObject);
        }

        #region Model Tests

        [Test]
        public void SetNextLevelData_IncrementsIndex()
        {
            // Arrange
            _model.SetLevelCollection(_testLevelCollection);
            _model.CurrentLevelDataIndex = 0;

            // Act
            _model.SetNextLevelData();

            // Assert
            Assert.AreEqual(1, _model.CurrentLevelDataIndex);
        }

        [Test]
        public void SetPreviousLevelData_DecrementsIndex()
        {
            // Arrange
            _model.SetLevelCollection(_testLevelCollection);
            _model.CurrentLevelDataIndex = 1;

            // Act
            _model.SetPreviousLevelData();

            // Assert
            Assert.AreEqual(0, _model.CurrentLevelDataIndex);
        }

        [Test]
        public void IsCurrentLevelUnlocked_ReturnsCorrectStatus()
        {
            // Arrange
            _model.SetLevelCollection(_testLevelCollection);
            _model.SetUnlockedLevels(_testUnlockedLevels);
            _model.CurrentLevelData = _testLevelCollection.LevelItems[0];
            // Act & Assert
            Assert.IsTrue(_model.IsCurrentLevelUnlocked());

            // Arrange
            _model.CurrentLevelData = _testLevelCollection.LevelItems[1];

            // Act & Assert
            Assert.IsFalse(_model.IsCurrentLevelUnlocked());
        }

        [Test]
        public void GetLog_ContainsRelevantInformation()
        {
            // Arrange
            _model.SetLevelCollection(_testLevelCollection);
            _model.SetUnlockedLevels(_testUnlockedLevels);
            _model.CurrentLevelData = _testLevelCollection.LevelItems[0];

            // Act
            var log = _model.GetLog();

            // Assert
            StringAssert.Contains("Level1", log);
            StringAssert.Contains("CurrentLevelDataIndex", log);
            StringAssert.Contains("Unlocked Level:", log);
        }

        #endregion

        #region Controller Tests

        [Test]
        public void Init_WithValidData_SetsModelProperties()
        {
            // Act
            _controller.InitModel(_testLevelData);

            // Assert
            Assert.AreEqual(_testLevelCollection, _model.LevelCollection);
            Assert.AreEqual(_testUnlockedLevels, _model.UnlockedLevels);
        }

        [Test]
        public void Init_WithNullLevelData_LogsError()
        {
            // Act & Assert
            LogAssert.Expect(LogType.Error, "LEVELDATA IS NULL");
            _controller.InitModel(null);
        }

        [Test]
        public void SetView_InitializesCallbacksAndModel()
        {
            // Arrange
            _controller.InitModel(_testLevelData);

            // Act
            _controller.SetView(_view);

            // Assert
            Assert.IsNotNull(TestUtility.GetPrivateField<UnityAction>(_view, "_onPlay"));
            Assert.IsNotNull(TestUtility.GetPrivateField<UnityAction>(_view, "_onMainMenu"));
            Assert.IsNotNull(TestUtility.GetPrivateField<UnityAction>(_view, "_onNext"));
            Assert.IsNotNull(TestUtility.GetPrivateField<UnityAction>(_view, "_onPrevious"));
            Assert.AreEqual(_model, TestUtility.GetPrivateField<ILevelSelectionPlayerModel>(_view, "_model"));
        }

        [Test]
        public void OnNext_UpdatesCurrentLevelData()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            var initialIndex = _model.CurrentLevelDataIndex;

            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnNext", null);

            // Assert
            Assert.AreEqual(initialIndex + 1, _model.CurrentLevelDataIndex);
            Assert.AreEqual(_testLevelCollection.LevelItems[1], _model.CurrentLevelData);
        }

        [Test]
        public void OnNext_WrapsToFirstWhenExceedingCount()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            _model.CurrentLevelDataIndex = _testLevelCollection.LevelItems.Count - 1;
            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnNext", null);

            // Assert
            Assert.AreEqual(0, _model.CurrentLevelDataIndex);
        }

        [Test]
        public void OnPrevious_UpdatesCurrentLevelData()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            _model.CurrentLevelDataIndex = 1;

            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnPrevious", null);

            // Assert
            Assert.AreEqual(0, _model.CurrentLevelDataIndex);
            Assert.AreEqual(_testLevelCollection.LevelItems[0], _model.CurrentLevelData);
        }

        [Test]
        public void OnPrevious_WrapsToLastWhenBelowZero()
        {
            // Arrange
            _controller.InitModel(_testLevelData);
            _model.CurrentLevelDataIndex = 0;

            // Act
            TestUtility.InvokePrivateMethod(_controller, "OnPrevious", null);

            // Assert
            Assert.AreEqual(_testLevelCollection.LevelItems.Count - 1, _model.CurrentLevelDataIndex);
        }

        #endregion

        

        #region Helper Methods

        private List<Transform> CreateTestModels()
        {
            var models = new List<Transform>();
            for (int i = 0; i < 2; i++)
            {
                var go = new GameObject($"Model{i}");
                models.Add(go.transform);
            }
            return models;
        }

        private class TestLevelDataModel : ILevelDataModel
        {
            public SOLevelCollection LevelCollection { get; set; }
            public List<SOLevelData> UnlockedLevels { get; set; }

            public SOCutsceneData CurrentCutsceneData { get; set; }

            public SOLevelData CurrentLevelData { get; set; }

            public SavedLevelData SavedLevelData { get; set; }

            public GameObject CurrentEnvironmentPrefab { get; set; }

            public event OnDataModified OnDataModified;

            public List<SOLevelData> GetUnlockedLevels() => UnlockedLevels;

            public bool IsMemberValid()
            {
                return Validator.ValidateNotNull(LevelCollection, "LEVELCOLLECTION IS NULL") &&
                    Validator.ValidateNotNull(CurrentLevelData, "CURRENTLEVELDATA IS NULL") &&
                    Validator.ValidateNotNull(CurrentCutsceneData, "CURRENTCUTSCENEDATA IS NULL") &&
                    Validator.ValidateNotNull(CurrentCutsceneData, "CURRENTCUTSCENEDATA IS NULL") &&
                    Validator.ValidateCollection(GetUnlockedLevels(), "UNLOCKEDLEVELS IS NULL");
            }
        }

        #endregion
    }
}