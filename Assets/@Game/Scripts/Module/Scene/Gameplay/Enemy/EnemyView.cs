using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.Enemy
{
    public class EnemyView : BaseView
    {
        public Transform player;          // The player's transform
        public float speed = 5f;          // Movement speed of the enemy
        public float rayDistance = 2f;    // Distance for the raycast to detect obstacles
        public float rayAngle = 30f;      // Angle to spread the rays to detect obstacles
        public float rotationSpeed = 5f;  // Speed of rotation towards movement direction
        private Vector3 movementDirection; // The direction the enemy should move in

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            // Optional: Find the player automatically if not set in the Inspector
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        private void FixedUpdate()
        {
            if (player == null)
            {
                Debug.LogError("Player not assigned or found!");
                return;
            }

            FollowPlayer();
            AvoidObstacles();
            Move();
            RotateTowardsMovementDirection();
        }

        private void FollowPlayer()
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Set the movement direction to the direction towards the player
            movementDirection = directionToPlayer;
        }

        private void AvoidObstacles()
        {
            // Cast a ray directly in front of the enemy
            if (Physics.Raycast(transform.position, transform.forward, rayDistance))
            {
                // If there's an obstacle in front, calculate a new direction to avoid it
                movementDirection = GetAvoidanceDirection();
            }
        }

        private void Move()
        {
            // Move the enemy in the determined direction (towards player, avoiding obstacles)
            Vector3 newPosition = rb.position + movementDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);  // Move the Rigidbody using physics
        }

        private Vector3 GetAvoidanceDirection()
        {
            // Cast rays at an angle to the left and right to detect an open path
            Vector3 leftDirection = Quaternion.Euler(0, -rayAngle, 0) * transform.forward;
            Vector3 rightDirection = Quaternion.Euler(0, rayAngle, 0) * transform.forward;

            // Check if the left direction is clear
            if (!Physics.Raycast(transform.position, leftDirection, rayDistance))
            {
                return leftDirection;
            }
            // Check if the right direction is clear
            else if (!Physics.Raycast(transform.position, rightDirection, rayDistance))
            {
                return rightDirection;
            }
            else
            {
                // If both directions are blocked, move backward or keep following the player as a fallback
                return -transform.forward;
            }
        }

        private void RotateTowardsMovementDirection()
        {
            // Rotate the enemy to face the direction of movement
            if (movementDirection != Vector3.zero)
            {
                // Calculate the desired rotation based on the movement direction
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

                // Smoothly rotate towards the target rotation using Lerp
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        // Draw Gizmos in the scene to visualize rays for debugging
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;

            // Draw forward ray (for obstacle detection)
            Gizmos.DrawRay(transform.position, transform.forward * rayDistance);

            // Draw left and right rays for avoidance
            Vector3 leftDirection = Quaternion.Euler(0, -rayAngle, 0) * transform.forward;
            Vector3 rightDirection = Quaternion.Euler(0, rayAngle, 0) * transform.forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, leftDirection * rayDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, rightDirection * rayDistance);
        }
    }
}