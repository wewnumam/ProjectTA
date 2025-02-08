using Agate.MVC.Core;
using NUnit.Framework;
using ProjectTA.Module.QuestData;
using ProjectTA.Module.QuestList;
using ProjectTA.Utility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class QuestListTests
    {
        private QuestListController _controller;
        private QuestDataModel _model;
        private QuestListView _view;
        private SOQuestCollection _testQuestCollection;
        private SavedQuestData _testQuestData;

        [SetUp]
        public void Setup()
        {
            // Initialize controller and model
            _controller = new QuestListController();
            _model = new QuestDataModel();

            // Create test quest collection
            var questItem1 = new QuestItem
            {
                Label = "Kill Enemies",
                Type = EnumManager.QuestType.Kill,
                RequiredAmount = 10
            };
            var questItem2 = new QuestItem
            {
                Label = "Collect Items",
                Type = EnumManager.QuestType.Collectible,
                RequiredAmount = 5
            };

            _testQuestCollection = ScriptableObject.CreateInstance<SOQuestCollection>();
            _testQuestCollection.QuestItems = new List<QuestItem> { questItem2, questItem1 };

            // Create test quest data
            _testQuestData = new SavedQuestData
            {
                CurrentKillAmount = 7,
                CurrentCollectibleAmount = 3
            };

            // Set the model for the controller
            _controller.SetModel(_model);

            // Create and setup view
            QuestComponent questComponent = new GameObject().AddComponent<QuestComponent>();
            questComponent.Slider = new GameObject().AddComponent<Slider>();
            questComponent.LabelText = new GameObject().AddComponent<TextMeshProUGUI>();
            questComponent.ProgressText = new GameObject().AddComponent<TextMeshProUGUI>();
            _view = new GameObject().AddComponent<QuestListView>();
            _view.Parent = new GameObject().transform;
            _view.PointsText = _view.gameObject.AddComponent<TextMeshProUGUI>();
            _view.QuestComponentTemplate = questComponent;
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_view.gameObject);
        }

        #region Model Tests

        [Test]
        public void InitModel_WithValidData_SetsModelProperties()
        {
            // Arrange
            var questData = new TestQuestDataModel
            {
                QuestCollection = _testQuestCollection,
                CurrentQuestData = _testQuestData
            };

            // Act
            _controller.InitModel(questData);

            // Assert
            Assert.AreEqual(_testQuestCollection, _model.QuestCollection);
            Assert.AreEqual(_testQuestData, _model.CurrentQuestData);
        }

        [Test]
        public void InitModel_WithNullQuestData_LogsError()
        {
            // Act & Assert
            LogAssert.Expect(LogType.Error, "QUESTDATA IS NULL");
            _controller.InitModel(null);
        }

        [Test]
        public void InitModel_WithNullQuestCollection_LogsError()
        {
            // Arrange
            var questData = new TestQuestDataModel
            {
                QuestCollection = null,
                CurrentQuestData = _testQuestData
            };

            // Act & Assert
            LogAssert.Expect(LogType.Error, "QUESTCOLLECTION IS NULL");
            _controller.InitModel(questData);
        }

        [Test]
        public void InitModel_WithNullCurrentQuestData_LogsError()
        {
            // Arrange
            var questData = new TestQuestDataModel
            {
                QuestCollection = _testQuestCollection,
                CurrentQuestData = null
            };

            // Act & Assert
            LogAssert.Expect(LogType.Error, "CURRENTQUESTDATA IS NULL");
            _controller.InitModel(questData);
        }

        #endregion

        #region View Tests

        [Test]
        public void SetView_InitializesQuestComponentsCorrectly()
        {
            // Arrange
            _controller.InitModel(new TestQuestDataModel
            {
                QuestCollection = _testQuestCollection,
                CurrentQuestData = _testQuestData
            });

            // Act
            _controller.SetView(_view);

            // Assert
            Assert.AreEqual(2, _view.Parent.childCount); // Two quest items should be instantiated
            var firstQuestComponent = _view.Parent.GetChild(0).GetComponent<QuestComponent>();
            Assert.IsNotNull(firstQuestComponent);
            Assert.AreEqual("Kill Enemies", firstQuestComponent.LabelText.text);
            Assert.AreEqual(7f, firstQuestComponent.Slider.value);
            Assert.AreEqual("7/10", firstQuestComponent.ProgressText.text);
        }

        [Test]
        public void SetView_CalculatesPointsCorrectly()
        {
            // Arrange
            _controller.InitModel(new TestQuestDataModel
            {
                QuestCollection = _testQuestCollection,
                CurrentQuestData = _testQuestData
            });

            // Act
            _controller.SetView(_view);

            // Assert
            Assert.AreEqual("130", _view.PointsText.text); // (7/10)*100 + (3/5)*100 = 70 + 25 = 95
        }

        #endregion

        #region Helper Method Tests

        [Test]
        public void GetCurrentAmount_ReturnsCorrectAmountForKillType()
        {
            // Arrange
            var questItem = new QuestItem { Type = EnumManager.QuestType.Kill, RequiredAmount = 10 };
            _model.SetCurrentQuestData(_testQuestData);

            // Act
            float amount = _controller.GetCurrentAmount(questItem);

            // Assert
            Assert.AreEqual(7, amount);
        }

        [Test]
        public void GetCurrentAmount_ReturnsCorrectAmountForCollectibleType()
        {
            // Arrange
            var questItem = new QuestItem { Type = EnumManager.QuestType.Collectible, RequiredAmount = 5 };
            _model.SetCurrentQuestData(_testQuestData);

            // Act
            float amount = _controller.GetCurrentAmount(questItem);

            // Assert
            Assert.AreEqual(3, amount);
        }

        [Test]
        public void GetCurrentAmount_ReturnsZeroForUnsupportedType()
        {
            // Arrange
            var questItem = new QuestItem { Type = (EnumManager.QuestType)999, RequiredAmount = 5 }; // Unsupported type
            _model.SetCurrentQuestData(_testQuestData);

            // Act
            float amount = _controller.GetCurrentAmount(questItem);

            // Assert
            Assert.AreEqual(0, amount);
        }

        #endregion

        #region Helper Classes

        private class TestQuestDataModel : IQuestDataModel
        {
            public SOQuestCollection QuestCollection { get; set; }
            public SavedQuestData CurrentQuestData { get; set; }

            public event OnDataModified OnDataModified;
        }

        #endregion
    }
}