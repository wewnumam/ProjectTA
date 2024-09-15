using Agate.MVC.Base;
using Agate.MVC.Core;
using NaughtyAttributes;
using ProjectTA.Module.Input;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterView : BaseView
    {
        public float speed;
        public Rigidbody rb;
        public Camera mainCamera;
        [ReadOnly] public Vector2 direction;

        private void FixedUpdate()
        {
            RotateTowardsMouse();
            MovePlayerCharacter();
        }

        private void MovePlayerCharacter()
        {
            rb.velocity = new Vector3(direction.x, 0, direction.y).normalized * speed;
        }

        void RotateTowardsMouse()
        {
            // Get the mouse position in the world
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // Calculate direction from player to mouse hit point
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                direction.y = 0; // Keep the player upright

                // Rotate player towards the mouse direction
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    rb.MoveRotation(targetRotation);
                }
            }
        }
    }
}