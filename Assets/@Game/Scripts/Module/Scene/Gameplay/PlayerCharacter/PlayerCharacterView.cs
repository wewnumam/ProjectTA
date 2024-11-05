using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Module.Dialogue;
using ProjectTA.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterView : BaseView
    {
        public float speed;
        public Rigidbody rb;
        public Camera mainCamera;
        [ReadOnly] public Vector2 direction;
        [ReadOnly] public Vector2 aim;
        [ReadOnly] public bool isJoystickActive;

        public float rayDistance = 100f;  // Distance of the raycast
        public LayerMask enemyLayer;      // Assign layer for enemies
        public LineRenderer lineRenderer; // LineRenderer component
        public float fixedYPosition = 1f; // The fixed Y position for the ray and line
        public Animator animator;

        private UnityAction onCollideWithEnemy;
        private UnityAction<TextAsset> onCollideWithDialogueComponent;
        private UnityAction onCollideWithPadlock;

        void Update()
        {
            // Create a ray starting from the player's position with fixed Y and forward direction
            Vector3 rayOrigin = new Vector3(transform.position.x, fixedYPosition, transform.position.z);
            Vector3 rayDirection = new Vector3(transform.forward.x, 0, transform.forward.z); // Ensure direction is parallel to the ground

            Ray ray = new Ray(rayOrigin, rayDirection);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, rayDistance, enemyLayer))
            {
                // Check if the object hit is tagged as "Enemy"
                if (hit.collider.CompareTag(TagManager.TAG_ENEMY))
                {
                    // Enable the LineRenderer and set the positions
                    lineRenderer.enabled = true;

                    // Start of the line (player's position with fixed Y)
                    lineRenderer.SetPosition(0, rayOrigin);

                    // End of the line (hit point with fixed Y)
                    Vector3 hitPointFixedY = new Vector3(hit.point.x, fixedYPosition, hit.point.z);
                    lineRenderer.SetPosition(1, hitPointFixedY);
                }
            }
            else
            {
                // Disable the LineRenderer if no enemy is hit
                lineRenderer.enabled = false;
            }
        }

        private void FixedUpdate()
        {
            if (isJoystickActive)
                RotatePlayerCharacter();
            else
                RotateTowardsMouse();
            
            MovePlayerCharacter();
        }

        private void MovePlayerCharacter()
        {
            rb.velocity = new Vector3(direction.x, 0, direction.y).normalized * speed;
        }

        private void RotatePlayerCharacter()
        {
            if (aim.sqrMagnitude > 0.1f)
            {
                // Calculate the angle to rotate the player
                float targetAngle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg;

                // Smoothly rotate the player towards the target angle
                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            }
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<DialogueComponent>(out var dialogue))
            {
                onCollideWithDialogueComponent?.Invoke(dialogue.dialogueAsset);
            }

            if (collision.gameObject.CompareTag(TagManager.TAG_ENEMY))
                onCollideWithEnemy?.Invoke();

            if (collision.gameObject.CompareTag(TagManager.TAG_PADLOCK))
                onCollideWithPadlock?.Invoke();
        }

        public void SetCollideCallbacks(UnityAction onCollideWithEnemy, UnityAction<TextAsset> onCollideWithDialogueComponent, UnityAction onCollideWithPadlock)
        {
            this.onCollideWithEnemy = onCollideWithEnemy;
            this.onCollideWithDialogueComponent = onCollideWithDialogueComponent;
            this.onCollideWithPadlock = onCollideWithPadlock;
        }
    }
}