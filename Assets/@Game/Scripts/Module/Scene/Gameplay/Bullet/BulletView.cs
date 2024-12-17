using Agate.MVC.Base;
using NaughtyAttributes;
using ProjectTA.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.Bullet
{
    public class BulletView : BaseView
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _launchForce;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private Renderer _bulletMeshRenderer;
        [SerializeField] private Material _initialHitMaterial;
        [SerializeField] private Material _normalHitMaterial;
        [SerializeField] private Material _enemyHitMaterial;

        private float _destroyDelay;
        private bool _hasHitTarget;
        private UnityAction _onCollideWithCollider;

        public float DestroyDelay => _destroyDelay;
        public Rigidbody Rb => _rb;
        public float LaunchForce => _launchForce;
        public GameObject HitEffect => _hitEffect;
        public Renderer BulletMeshRenderer => _bulletMeshRenderer;
        public Material InitialHitMaterial => _initialHitMaterial;
        public Material NormalHitMaterial => _normalHitMaterial;
        public Material EnemyHitMaterial => _enemyHitMaterial;

        public void SetCallback(UnityAction onCollideWithCollider)
        {
            _onCollideWithCollider = onCollideWithCollider;
        }

        public void SetDestroyDelay(float destroyDelay)
        {
            _destroyDelay = destroyDelay;
        }

        private void OnEnable()
        {
            _hasHitTarget = false;
            Rb.isKinematic = false;
            Rb.AddForce(transform.forward * LaunchForce, ForceMode.Impulse);
        }

        public void OnCollisionEnter(Collision collision)
        {
            // Check if the arrow has already hit something to prevent multiple hits
            if (_hasHitTarget) return;

            if (collision.gameObject.CompareTag(TagManager.TAG_ENEMY))
            {
                BulletMeshRenderer.material = EnemyHitMaterial;
            }
            else
            {
                BulletMeshRenderer.material = NormalHitMaterial;
            }

            _hasHitTarget = true;

            // Stop the arrow from continuing to move
            Rb.isKinematic = true;  // Stop all physics on the arrow
            Rb.velocity = Vector3.zero;  // Zero out its velocity

            // Stick the arrow into the target
            transform.position = collision.contacts[0].point;  // Set arrow to point of contact
            transform.parent = collision.transform;  // Attach arrow to the hit object

            // Optionally create a hit effect
            if (HitEffect != null)
            {
                Instantiate(HitEffect, transform.position, Quaternion.identity);
            }

            // Destroy the arrow after a delay (if you want to remove the arrow)
            Invoke(nameof(OnCollideWithCollider), DestroyDelay);
        }

        public void OnCollideWithCollider()
        {
            _onCollideWithCollider?.Invoke();
        }
    }
}