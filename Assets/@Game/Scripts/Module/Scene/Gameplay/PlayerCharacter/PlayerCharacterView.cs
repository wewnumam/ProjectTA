using Agate.MVC.Base;
using ProjectTA.Module.CollectibleData;
using ProjectTA.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ProjectTA.Module.PlayerCharacter
{
    public class PlayerCharacterView : BaseView
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private float _rayDistance = 100f;
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _fixedYPosition = 1f;

        private Vector2 _direction;
        private Vector2 _aim;
        private Vector2 _smoothedAim;
        private bool _isJoystickActive;

        private UnityAction _onCollideWithEnemy;
        private UnityAction<SOCollectibleData> _onCollideWithCollectibleComponent;
        private UnityAction _onCollideWithPadlock;

        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;

        void Update()
        {
            // Create a ray starting from the player's position with fixed Y and forward direction
            Vector3 rayOrigin = new Vector3(transform.position.x, _fixedYPosition, transform.position.z);
            Vector3 rayDirection = new Vector3(transform.forward.x, 0, transform.forward.z); // Ensure direction is parallel to the ground

            Ray ray = new Ray(rayOrigin, rayDirection);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, _rayDistance, _enemyLayer))
            {
                // Check if the object hit is tagged as "Enemy"
                if (hit.collider.CompareTag(TagManager.TAG_ENEMY))
                {
                    // Enable the LineRenderer and set the positions
                    _lineRenderer.enabled = true;

                    // Start of the line (player's position with fixed Y)
                    _lineRenderer.SetPosition(0, rayOrigin);

                    // End of the line (hit point with fixed Y)
                    Vector3 hitPointFixedY = new Vector3(hit.point.x, _fixedYPosition, hit.point.z);
                    _lineRenderer.SetPosition(1, hitPointFixedY);
                }
                else
                {
                    // Disable the LineRenderer if no enemy is hit
                    _lineRenderer.enabled = false;
                }
            }
            
        }

        private void FixedUpdate()
        {
            if (_isJoystickActive)
                RotatePlayerCharacter(_aim);
            else
                RotateTowardsMouse();
            
            MovePlayerCharacter();
        }

        private void MovePlayerCharacter()
        {
            _rb.velocity = new Vector3(_direction.x, 0, _direction.y).normalized * _movementSpeed;
            if (_aim == Vector2.zero)
                RotatePlayerCharacter(_direction);
        }

        private void RotatePlayerCharacter(Vector2 aim)
        {
            _smoothedAim = Vector2.Lerp(_smoothedAim, aim, Time.fixedDeltaTime * _rotationSpeed); // Adjust the interpolation factor as needed

            // Check if there is significant input to process
            if (aim.sqrMagnitude > 0.2f)
            {
                Vector3 direction = new Vector3(_smoothedAim.x, 0, _smoothedAim.y);
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _rb.MoveRotation(targetRotation);
            }
        }

        void RotateTowardsMouse()
        {
            // Get the mouse position in the world
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // Calculate direction from player to mouse hit point
                Vector3 direction = (hitInfo.point - transform.position).normalized;
                direction.y = 0; // Keep the player upright

                // Rotate player towards the mouse direction
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    _rb.MoveRotation(targetRotation);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.TAG_ENEMY))
                _onCollideWithEnemy?.Invoke();

            if (collision.gameObject.CompareTag(TagManager.TAG_PADLOCK))
                _onCollideWithPadlock?.Invoke();

            if (!collision.gameObject.CompareTag(TagManager.TAG_COLLECTIBLE))
            {
                return;
            }

            if (collision.gameObject.TryGetComponent<CollectibleComponent>(out var collectible))
            {
                _onCollideWithCollectibleComponent?.Invoke(collectible.CollectibleData);
                Destroy(collectible);
                collision.gameObject.SetActive(collectible.CollectibleData.Type == EnumManager.CollectibleType.HiddenObject);
            }
        }

        public void SetCollideCallbacks(
            UnityAction onCollideWithEnemy, 
            UnityAction<SOCollectibleData> onCollideWithCollectibleComponent, 
            UnityAction onCollideWithPadlock)
        {
            this._onCollideWithEnemy = onCollideWithEnemy;
            _onCollideWithCollectibleComponent = onCollideWithCollectibleComponent;
            this._onCollideWithPadlock = onCollideWithPadlock;
        }

        public void DisableKinematic()
        {
            _rb.isKinematic = false;
        }

        public void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        public void SetAim(Vector2 aim)
        {
            _aim = aim;
        }

        public void SetIsJoyStickActive(bool isJoyStickActive)
        {
            _isJoystickActive = isJoyStickActive;
        }
    }
}