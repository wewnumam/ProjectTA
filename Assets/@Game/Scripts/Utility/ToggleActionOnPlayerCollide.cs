using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Utility
{
    [RequireComponent(typeof(SphereCollider))]
    public class ToggleActionOnPlayerCollide : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onPlayerEnter;
        [SerializeField] private UnityEvent _onPlayerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TagManager.TAG_PLAYER))
            {
                _onPlayerEnter?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(TagManager.TAG_PLAYER))
            {
                _onPlayerExit?.Invoke();
            }
        }
    }
}