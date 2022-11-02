using UnityEngine;
using UnityEngine.Events;

namespace GithubGameOff2022.Player
{
    public class TriggerDetector : MonoBehaviour
    {
        public UnityEvent<Collider> TriggerEnterEvent { get; } = new();
        public UnityEvent<Collider> TriggerExitEvent { get; } = new();

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnterEvent.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExitEvent.Invoke(other);
        }
    }
}
