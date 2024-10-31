using ProjectTA.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectTA.Module.PuzzleBoard
{
    public class PuzzleComponent : MonoBehaviour
    {
        public UnityAction<PuzzleComponent> onSendPuzzleComponent;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(TagManager.TAG_PLAYER))
            {
                gameObject.SetActive(false);
                onSendPuzzleComponent?.Invoke(this);
            }
        }
    }
}