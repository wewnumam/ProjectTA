using NUnit.Framework;
using ProjectTA.Module.GameConstants;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    public class GameConstantsControllerTests
    {
        private GameConstantsController _controller;
        private GameConstantsModel _model;
        private SOGameConstants _gameConstants;

        private const string GAMECONSTANTS_NAME = "GameConstantsTest";

        [SetUp]
        public void Setup()
        {
            // Create the ScriptableObject instance
            _gameConstants = ScriptableObject.CreateInstance<SOGameConstants>();

            // Use reflection to set the private field
            var initialHealth = typeof(SOGameConstants).GetField("_initialHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            initialHealth.SetValue(_gameConstants, 100);
            var isJoystickActive = typeof(SOGameConstants).GetField("_isJoystickActive", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            isJoystickActive.SetValue(_gameConstants, true);

            AssetDatabase.CreateAsset(_gameConstants, $"Assets/Resources/{GAMECONSTANTS_NAME}.asset");

            _model = new GameConstantsModel();
            _model.SetGameConstants(_gameConstants);

            _controller = new GameConstantsController();
            _controller.SetModel(_model);
            _controller.SetFileName(GAMECONSTANTS_NAME);
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset($"Assets/Resources/{GAMECONSTANTS_NAME}.asset");
        }

        [UnityTest]
        public IEnumerator Initialize_ShouldSetGameConstants()
        {
            yield return _controller.Initialize();

            Assert.IsNotNull(_model.GameConstants);
            Assert.AreEqual(Resources.Load<SOGameConstants>(GAMECONSTANTS_NAME), _controller.Model.GameConstants);
            Assert.AreEqual(100, _model.GameConstants.InitialHealth);
            Assert.IsTrue(_model.GameConstants.IsJoystickActive);
        }
    }
}