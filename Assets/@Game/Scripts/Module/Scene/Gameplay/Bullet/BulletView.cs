using Agate.MVC.Base;
using UnityEngine;

namespace ProjectTA.Module.Bullet
{
    public class BulletView : BaseView
    {
        public Rigidbody rb;
        public float launchForce;
        public GameObject hitEffect;

        private bool hasHitTarget;

        private void Start()
        {
            rb.AddForce(transform.forward * launchForce, ForceMode.Impulse);
        }

        public  void OnCollisionEnter(Collision collision)
        {
            // Check if the arrow has already hit something to prevent multiple hits
            if (hasHitTarget) return;

            hasHitTarget = true;

            // Stop the arrow from continuing to move
            rb.isKinematic = true;  // Stop all physics on the arrow
            rb.velocity = Vector3.zero;  // Zero out its velocity

            // Stick the arrow into the target
            transform.position = collision.contacts[0].point;  // Set arrow to point of contact
            transform.parent = collision.transform;  // Attach arrow to the hit object

            // Optionally create a hit effect
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            // Destroy the arrow after a delay (if you want to remove the arrow)
            Destroy(gameObject, 5f);
        }
    }
}