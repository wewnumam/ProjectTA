using NUnit.Framework;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Utility;
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace ProjectTA.Tests
{
    public class PlayerCharacterTest
    {
        private GameObject _playerObject; private PlayerCharacterView _playerCharacter;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneManager.LoadScene(TagManager.SCENE_GAMEPLAY);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == TagManager.SCENE_GAMEPLAY);

            _playerObject = GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER);
            Assert.IsNotNull(_playerObject, "Player object could not be found.");

            _playerCharacter = _playerObject.GetComponent<PlayerCharacterView>();
            Assert.IsNotNull(_playerCharacter, "PlayerCharacterView could not be found on the player object.");
        }

        [UnityTest]
        public IEnumerator MovePlayerCharacterWithKeyboard()
        {
            var keyboard = InputSystem.AddDevice<Keyboard>();
            yield return PerformKeyPressTest(keyboard);
        }

        [UnityTest]
        public IEnumerator MovePlayerCharacterWithGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            yield return PerformGamepadMovementTest(gamepad);
        }

        [UnityTest]
        public IEnumerator AimPlayerCharacterWithGamepad()
        {
            var gamepad = InputSystem.AddDevice<Gamepad>();
            yield return PerformGamepadAimTest(gamepad);
        }

        private IEnumerator PerformKeyPressTest(Keyboard keyboard)
        {
            yield return new WaitForSeconds(5);

            foreach (var key in new[] { Key.W, Key.A, Key.S, Key.D })
            {
                InputSystem.QueueStateEvent(keyboard, new KeyboardState(key));
                InputSystem.Update();
                yield return new WaitForSeconds(1);
                Assert.IsTrue(keyboard[key].isPressed, $"{key} key press failed.");
            }
        }

        private IEnumerator PerformGamepadMovementTest(Gamepad gamepad)
        {
            yield return PerformGamepadStickTest(gamepad, new Vector2(0, 1), "up");
            yield return PerformGamepadStickTest(gamepad, new Vector2(-1, 0), "left");
            yield return PerformGamepadStickTest(gamepad, new Vector2(0, -1), "down");
            yield return PerformGamepadStickTest(gamepad, new Vector2(1, 0), "right");
        }

        private IEnumerator PerformGamepadAimTest(Gamepad gamepad)
        {
            yield return PerformGamepadStickTest(gamepad, new Vector2(0, 1), "aim up");
            yield return PerformGamepadStickTest(gamepad, new Vector2(-1, 0), "aim left");
            yield return PerformGamepadStickTest(gamepad, new Vector2(0, -1), "aim down");
            yield return PerformGamepadStickTest(gamepad, new Vector2(1, 0), "aim right");
        }

        private IEnumerator PerformGamepadStickTest(Gamepad gamepad, Vector2 direction, string action)
        {
            InputSystem.QueueStateEvent(gamepad, new GamepadState { leftStick = direction });
            InputSystem.Update();
            yield return new WaitForSeconds(1);
            Assert.AreEqual(direction, gamepad.leftStick.ReadValue(), $"Gamepad left stick {action} movement failed.");
        }

        [Test, Performance]
        public void PerformanceTest_MovePlayerCharacterWithGamepad()
        {
            Measure.Method(() =>
            {
                var gamepad = InputSystem.AddDevice<Gamepad>();
                PerformGamepadMovementTest(gamepad).MoveNext();
            })
            .WarmupCount(10)
            .MeasurementCount(10)
            .IterationsPerMeasurement(5)
            .GC()
            .Run();
        }
    }
}