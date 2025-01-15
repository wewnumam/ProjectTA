using Agate.MVC.Base;
using ProjectTA.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.Enemy
{
    public class EnemyView : BaseView
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rayDistance = 2f;
        [SerializeField] private float _rayAngle = 30f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] ParticleSystem _spawnFX;
        [SerializeField] ParticleSystem _dieFX;

        private bool _isPause;
        private bool _isDie;
        private float _destroyDelay;
        private Transform _player;
        private Vector3 _movementDirection;
        private Rigidbody _rb;
        private UnityAction _onKill;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _isDie = false;

            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag(TagManager.TAG_PLAYER).transform;
            }

            if (_spawnFX != null)
            {
                _spawnFX.Play();
            }
        }

        private void FixedUpdate()
        {
            if (_player == null)
            {
                Debug.LogError("Player not assigned or found!");
                return;
            }

            if (_isPause || _isDie)
                return;

            FollowPlayer();
            AvoidObstacles();
            Move();
            RotateTowardsMovementDirection();
        }

        public void SetPause(bool isPause)
        {
            _isPause = isPause;
        }

        public void SetPlayer(Transform player)
        {
            _player = player;
        }

        public void SetDestroyDelay(float destroyDelay)
        {
            _destroyDelay = destroyDelay;
        }

        public void SetCallback(UnityAction onKill)
        {
            _onKill = onKill;
        }

        private void FollowPlayer()
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (_player.position - transform.position).normalized;

            // Set the movement direction to the direction towards the player
            _movementDirection = directionToPlayer;
        }

        private void AvoidObstacles()
        {
            // Cast a ray directly in front of the enemy
            if (Physics.Raycast(transform.position, transform.forward, _rayDistance))
            {
                // If there's an obstacle in front, calculate a new direction to avoid it
                _movementDirection = GetAvoidanceDirection();
            }
        }

        private void Move()
        {
            // Move the enemy in the determined direction (towards player, avoiding obstacles)
            Vector3 newPosition = _rb.position + _movementDirection * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);  // Move the Rigidbody using physics
        }

        private Vector3 GetAvoidanceDirection()
        {
            // Cast rays at an angle to the left and right to detect an open path
            Vector3 leftDirection = Quaternion.Euler(0, -_rayAngle, 0) * transform.forward;
            Vector3 rightDirection = Quaternion.Euler(0, _rayAngle, 0) * transform.forward;

            // Check if the left direction is clear
            if (!Physics.Raycast(transform.position, leftDirection, _rayDistance))
            {
                return leftDirection;
            }
            // Check if the right direction is clear
            else if (!Physics.Raycast(transform.position, rightDirection, _rayDistance))
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
            if (_movementDirection != Vector3.zero)
            {
                // Calculate the desired rotation based on the movement direction
                Quaternion targetRotation = Quaternion.LookRotation(_movementDirection);

                // Smoothly rotate towards the target rotation using Lerp
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.TAG_BULLET) && !_isDie)
            {
                if (_dieFX != null)
                {
                    _dieFX.Play();
                }
                _isDie = true;
                Invoke(nameof(Kill), _destroyDelay);
            }
        }

        private void Kill()
        {
            _onKill?.Invoke();
            gameObject.SetActive(false);
        }

        // Draw Gizmos in the scene to visualize rays for debugging
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;

            // Draw forward ray (for obstacle detection)
            Gizmos.DrawRay(transform.position, transform.forward * _rayDistance);

            // Draw left and right rays for avoidance
            Vector3 leftDirection = Quaternion.Euler(0, -_rayAngle, 0) * transform.forward;
            Vector3 rightDirection = Quaternion.Euler(0, _rayAngle, 0) * transform.forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, leftDirection * _rayDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, rightDirection * _rayDistance);
        }
    }
}