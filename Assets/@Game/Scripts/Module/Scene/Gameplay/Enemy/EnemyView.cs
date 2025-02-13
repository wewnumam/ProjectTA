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
        [SerializeField] private ParticleSystem _spawnFX;
        [SerializeField] private ParticleSystem _dieFX;
        [SerializeField] private Animator _animator; // Reference to the Animator component

        private bool _isPause;
        private bool _isDie;
        private bool _isAttack;
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
            if (_animator != null)
            {
                _animator.Play("Walk");
            }
        }

        private void FixedUpdate()
        {
            if (_isPause || _isDie)
                return;

            if (_player == null)
            {
                Debug.LogWarning("Player not assigned or found!");
                return;
            }

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
            Vector3 directionToPlayer = (_player.position - transform.position).normalized;
            _movementDirection = directionToPlayer;
        }

        private void AvoidObstacles()
        {
            if (Physics.Raycast(transform.position, transform.forward, _rayDistance))
            {
                _movementDirection = GetAvoidanceDirection();
            }
        }

        private void Move()
        {
            Vector3 newPosition = _rb.position + _movementDirection * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }

        private Vector3 GetAvoidanceDirection()
        {
            Vector3 leftDirection = Quaternion.Euler(0, -_rayAngle, 0) * transform.forward;
            Vector3 rightDirection = Quaternion.Euler(0, _rayAngle, 0) * transform.forward;

            if (!Physics.Raycast(transform.position, leftDirection, _rayDistance))
            {
                return leftDirection;
            }
            else if (!Physics.Raycast(transform.position, rightDirection, _rayDistance))
            {
                return rightDirection;
            }
            else
            {
                return -transform.forward;
            }
        }

        private void RotateTowardsMovementDirection()
        {
            if (_movementDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_movementDirection);
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

                // Trigger Die animation
                if (_animator != null)
                {
                    _animator.Play("Die");
                }

                Invoke(nameof(Kill), _destroyDelay);
            }

            if (collision.gameObject.CompareTag(TagManager.TAG_PLAYER))
            {
                _animator.Play("Attack");
            }
        }

        private void Kill()
        {
            _onKill?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * _rayDistance);

            Vector3 leftDirection = Quaternion.Euler(0, -_rayAngle, 0) * transform.forward;
            Vector3 rightDirection = Quaternion.Euler(0, _rayAngle, 0) * transform.forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, leftDirection * _rayDistance);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, rightDirection * _rayDistance);
        }
    }
}