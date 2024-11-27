using System.Collections;
using NUnit.Framework;
using ProjectTA.Module.PlayerCharacter;
using ProjectTA.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayerCharacterTest
{
    private GameObject _playerObject;
    private PlayerCharacterView _playerCharacter;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        SceneManager.LoadScene(TagManager.SCENE_GAMEPLAY);

        // Wait until the scene is loaded
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
        
        yield return new WaitForSeconds(5);

        // Simulate W key press
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.W));
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.IsTrue(keyboard.wKey.isPressed);

        // Simulate A key press
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.A));
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.IsTrue(keyboard.aKey.isPressed);

        // Simulate S key press
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.S));
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.IsTrue(keyboard.sKey.isPressed);

        // Simulate D key press
        InputSystem.QueueStateEvent(keyboard, new KeyboardState(Key.D));
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.IsTrue(keyboard.dKey.isPressed);
    }

    [UnityTest]
    public IEnumerator MovePlayerCharacterWithGamepad()
    {
        var gamepad = InputSystem.AddDevice<Gamepad>();

        // Simulate left stick movement upwards (W equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { leftStick = new Vector2(0, 1) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(0, 1), gamepad.leftStick.ReadValue(), "Gamepad left stick up movement failed.");

        // Simulate left stick movement leftwards (A equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { leftStick = new Vector2(-1, 0) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(-1, 0), gamepad.leftStick.ReadValue(), "Gamepad left stick left movement failed.");

        // Simulate left stick movement downwards (S equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { leftStick = new Vector2(0, -1) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(0, -1), gamepad.leftStick.ReadValue(), "Gamepad left stick down movement failed.");

        // Simulate left stick movement rightwards (D equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { leftStick = new Vector2(1, 0) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(1, 0), gamepad.leftStick.ReadValue(), "Gamepad left stick right movement failed.");
    }

    [UnityTest]
    public IEnumerator AimPlayerCharacterWithGamepad()
    {
        var gamepad = InputSystem.AddDevice<Gamepad>();

        // Simulate left stick movement upwards (W equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { rightStick = new Vector2(0, 1) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(0, 1), gamepad.rightStick.ReadValue(), "Gamepad left stick up movement failed.");

        // Simulate left stick movement leftwards (A equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { rightStick = new Vector2(-1, 0) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(-1, 0), gamepad.rightStick.ReadValue(), "Gamepad left stick left movement failed.");

        // Simulate left stick movement downwards (S equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { rightStick = new Vector2(0, -1) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(0, -1), gamepad.rightStick.ReadValue(), "Gamepad left stick down movement failed.");

        // Simulate left stick movement rightwards (D equivalent)
        InputSystem.QueueStateEvent(gamepad, new GamepadState { rightStick = new Vector2(1, 0) });
        InputSystem.Update();

        yield return new WaitForSeconds(1);
        Assert.AreEqual(new Vector2(1, 0), gamepad.rightStick.ReadValue(), "Gamepad left stick right movement failed.");
    }
}
