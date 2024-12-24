using System.Collections.Generic;
using NUnit.Framework;
using ProjectTA.Module.LevelData;
using ProjectTA.Module.LevelSelection;
using UnityEngine;

namespace ProjectTA.Tests
{
    public class LevelSelectionPlayerModelTests
    {
        private LevelSelectionPlayerModel _model;
        private SOLevelCollection _levelCollection;
        private List<SOLevelData> _unlockedLevels;

        [SetUp]
        public void SetUp()
        {
            _model = new LevelSelectionPlayerModel();
            _levelCollection = ScriptableObject.CreateInstance<SOLevelCollection>();
            _unlockedLevels = new List<SOLevelData>();
        }

        [Test]
        public void SetLevelCollection_ShouldSetLevelCollection()
        {
            _model.SetLevelCollection(_levelCollection);
            Assert.AreEqual(_levelCollection, _model.LevelCollection);
        }

        [Test]
        public void SetUnlockedLevels_ShouldSetUnlockedLevels()
        {
            _model.SetUnlockedLevels(_unlockedLevels);
            Assert.AreEqual(_unlockedLevels, _model.UnlockedLevels);
        }

        [Test]
        public void IsCurrentLevelUnlocked_WhenLevelIsUnlocked_ShouldReturnTrue()
        {
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            _unlockedLevels.Add(levelData);
            _model.SetUnlockedLevels(_unlockedLevels);
            _model.SetCurrentLevelData(levelData);

            Assert.IsTrue(_model.IsCurrentLevelUnlocked());
        }

        [Test]
        public void IsCurrentLevelUnlocked_WhenLevelIsNotUnlocked_ShouldReturnFalse()
        {
            var levelData = ScriptableObject.CreateInstance<SOLevelData>();
            _model.SetCurrentLevelData(levelData);

            Assert.IsFalse(_model.IsCurrentLevelUnlocked());
        }

        [Test]
        public void SetNextLevelData_ShouldCycleThroughLevels()
        {
            var levelItems = new List<SOLevelData>()
            {
                ScriptableObject.CreateInstance<SOLevelData>(),
                ScriptableObject.CreateInstance<SOLevelData>(),
                ScriptableObject.CreateInstance<SOLevelData>()
            };
            var fieldInfo = typeof(SOLevelCollection).GetField("_levelItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(_levelCollection, levelItems);
            _model.SetLevelCollection(_levelCollection);
            _model.SetNextLevelData();
            Assert.AreEqual(1, _model.CurrentLevelDataIndex);
        }

        [Test]
        public void SetPreviousLevelData_ShouldCycleBackThroughLevels()
        {
            var levelItems = new List<SOLevelData>()
            {
                ScriptableObject.CreateInstance<SOLevelData>(),
                ScriptableObject.CreateInstance<SOLevelData>(),
                ScriptableObject.CreateInstance<SOLevelData>()
            };
            var fieldInfo = typeof(SOLevelCollection).GetField("_levelItems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            fieldInfo.SetValue(_levelCollection, levelItems);
            _model.SetLevelCollection(_levelCollection);
            _model.SetNextLevelData();
            _model.SetPreviousLevelData();
            Assert.AreEqual(0, _model.CurrentLevelDataIndex);
        }

        [Test]
        public void GetLog_ShouldReturnCorrectLog()
        {
            var log = _model.GetLog();
            Assert.IsTrue(log.Contains("LevelSelectionPlayerModel Log:"));
        }
    }
}