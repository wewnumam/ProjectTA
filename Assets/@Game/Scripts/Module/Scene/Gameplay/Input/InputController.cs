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
            inputActions.Character.Shoot.started += OnShootStart;
            inputActions.Character.Shoot.canceled += OnShootEnd;
            inputActions.Character.Aim.performed += OnStartAim;
            inputActions.Character.Aim.canceled += OnEndAim;
        }

        public override IEnumerator Terminate()
        {

            inputActions.Character.Move.performed -= OnStartMove;
            inputActions.Character.Move.canceled -= OnEndMove;
            inputActions.Character.Shoot.performed -= OnShootStart;
            inputActions.Character.Aim.performed -= OnStartAim;
            inputActions.Character.Aim.canceled -= OnEndAim;
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

        private void OnShootStart(InputAction.CallbackContext context)
        {
            Publish(new PlayerCharacterShootStartMessage());
        }
        private void OnShootEnd(InputAction.CallbackContext context)
        {
            Publish(new PlayerCharacterShootEndMessage());
        }


        private void OnStartAim(InputAction.CallbackContext context)
        {
            Publish(new RotatePlayerCharacterMessage(context.ReadValue<Vector2>()));
        }

        private void OnEndAim(InputAction.CallbackContext context)
        {
            Publish(new RotatePlayerCharacterMessage(Vector2.zero));
        }
    }
}
