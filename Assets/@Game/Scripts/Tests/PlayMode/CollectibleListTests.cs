using Agate.MVC.Core;
using NUnit.Framework;
using ProjectTA.Message;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Module.CollectibleItem;
using ProjectTA.Module.CollectibleList;
using ProjectTA.Utility;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace ProjectTA.Tests
{
    [TestFixture]
    public class CollectibleListTests
    {
        private CollectibleListController _controller;
        private CollectibleDataModel _model;
        private CollectibleListView _view;
        private SOCollectibleCollection _testCollectibleCollection;
        private List<SOCollectibleData> _testUnlockedCollectibleItems;

        [SetUp]
        public void Setup()
        {
            // Initialize controller and model
            _controller = new CollectibleListController();
            _model = new CollectibleDataModel();

            // Create test collectible collection
            var collectibleItem1 = ScriptableObject.CreateInstance<SOCollectibleData>();
            TestUtility.SetPrivateField(collectibleItem1, "_title", "Collectible 1");
            TestUtility.SetPrivateField(collectibleItem1, "_description", "Description for Collectible 1");

            var collectibleItem2 = ScriptableObject.CreateInstance<SOCollectibleData>();
            TestUtility.SetPrivateField(collectibleItem2, "_title", "Collectible 2");
            TestUtility.SetPrivateField(collectibleItem2, "_description", "Description for Collectible 2");

            _testCollectibleCollection = ScriptableObject.CreateInstance<SOCollectibleCollection>();
            TestUtility.SetPrivateField(_testCollectibleCollection, "_collectibleItems", new List<SOCollectibleData> { collectibleItem1, collectibleItem2 });

            // Create test unlocked collectible items
            _testUnlockedCollectibleItems = new List<SOCollectibleData> { collectibleItem1 };

            // Set the model for the controller
            _controller.SetModel(_model);

            // Create and setup view
            _view = new GameObject().AddComponent<CollectibleListView>();
            TestUtility.SetPrivateField(_view, "_parent", new GameObject().transform);
            var collectibleTemplate = new GameObject().AddComponent<CollectibleItemView>();
            TestUtility.SetPrivateField(collectibleTemplate, "_title", collectibleTemplate.gameObject.AddComponent<TextMeshProUGUI>());
            TestUtility.SetPrivateField(collectibleTemplate, "_chooseButton", collectibleTemplate.gameObject.AddComponent<Button>());
            TestUtility.SetPrivateField(_view, "_itemTemplate", collectibleTemplate);
            TestUtility.SetPrivateField(_view, "_descriptionText", _view.gameObject.AddComponent<TextMeshProUGUI>());
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
            var collectibleData = new TestCollectibleDataModel
            {
                CollectibleCollection = _testCollectibleCollection,
                UnlockedCollectibleItems = _testUnlockedCollectibleItems
            };

            // Act
            _controller.InitModel(collectibleData);

            // Assert
            Assert.AreEqual(_testCollectibleCollection, _model.CollectibleCollection);
            Assert.AreEqual(_testUnlockedCollectibleItems, _model.UnlockedCollectibleItems);
        }

        [Test]
        public void InitModel_WithNullCollectibleData_LogsError()
        {
            // Act & Assert
            LogAssert.Expect(LogType.Error, "COLLECTIBLEDATA IS NULL");
            _controller.InitModel(null);
        }

        [Test]
        public void InitModel_WithNullCollectibleCollection_LogsError()
        {
            // Arrange
            var collectibleData = new TestCollectibleDataModel
            {
                CollectibleCollection = null,
                UnlockedCollectibleItems = _testUnlockedCollectibleItems
            };

            // Act & Assert
            LogAssert.Expect(LogType.Error, "COLLECTIBLECOLLECTION IS NULL");
            _controller.InitModel(collectibleData);
        }

        [Test]
        public void InitModel_WithNullUnlockedCollectibleItems_LogsError()
        {
            // Arrange
            var collectibleData = new TestCollectibleDataModel
            {
                CollectibleCollection = _testCollectibleCollection,
                UnlockedCollectibleItems = null
            };

            // Act & Assert
            LogAssert.Expect(LogType.Error, "UNLOCKEDCOLLECTIBLEITEMS IS NULL");
            _controller.InitModel(collectibleData);
        }

        #endregion

        #region View Tests

        [Test]
        public void SetView_InitializesCollectibleItemsCorrectly()
        {
            // Arrange
            _controller.InitModel(new TestCollectibleDataModel
            {
                CollectibleCollection = _testCollectibleCollection,
                UnlockedCollectibleItems = _testUnlockedCollectibleItems
            });

            // Act
            _controller.SetView(_view);

            // Assert
            Assert.AreEqual(2, _view.Parent.childCount); // Two collectible items should be instantiated
            var firstCollectibleItemView = _view.Parent.GetChild(0).GetComponent<CollectibleItemView>();
            Assert.IsNotNull(firstCollectibleItemView);
            Assert.AreEqual("Collectible 1", firstCollectibleItemView.Title.text);
        }

        #endregion

        #region Message Handling Tests

        [Test]
        public void OnChooseColletible_UpdatesDescriptionText()
        {
            // Arrange
            _controller.InitModel(new TestCollectibleDataModel
            {
                CollectibleCollection = _testCollectibleCollection,
                UnlockedCollectibleItems = _testUnlockedCollectibleItems
            });
            _controller.SetView(_view);

            var message = new ChooseCollectibleMessage(_testCollectibleCollection.CollectibleItems[0]);

            // Act
            _controller.OnChooseColletible(message);

            // Assert
            Assert.AreEqual("Description for Collectible 1", _view.DescriptionText.text);
        }

        #endregion

        #region Helper Classes

        private class TestCollectibleDataModel : ICollectibleDataModel
        {
            public SOCollectibleCollection CollectibleCollection { get; set; }
            public List<SOCollectibleData> UnlockedCollectibleItems { get; set; }

            public event OnDataModified OnDataModified;
        }

        #endregion
    }
}