using Agate.MVC.Base;
using ProjectTA.Message;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTA.Module.Input
{
    public class InputController : BaseController<InputController>
    {
        InputActionManager inputActions;

        public override IEnumerator Initialize()
        {
            yield return base.Initialize();

            inputActions = new InputActionManager();
            inputActions.Character.Enable();
            inputActions.Character.Move.performed += OnStartMove;
            inputActions.Character.Move.canceled += OnEndMove;
            inputActions.Character.Shoot.performed += OnShoot;
        }

        public override IEnumerator Terminate()
        {

            inputActions.Character.Move.performed -= OnStartMove;
            inputActions.Character.Move.canceled -= OnEndMove;
            inputActions.Character.Shoot.performed -= OnShoot;
            inputActions.Character.Disable();
            
            yield return base.Terminate();
        }

        private void OnStartMove(InputAction.CallbackContext context)
        {
            Publish(new MovePlayerCharacterMessage(context.ReadValue<Vector2>()));
        }

        private void OnEndMove(InputAction.CallbackContext context)
        {
            Publish(new MovePlayerCharacterMessage(Vector2.zero));
        }

        private void OnShoot(InputAction.CallbackContext context)
        {
            Publish(new PlayerCharacterShootMessage());
        }
    }
}
