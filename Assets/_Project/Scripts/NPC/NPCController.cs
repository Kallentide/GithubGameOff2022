using UnityEngine;
using UnityEngine.AI;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour
    {
        private NavMeshAgent _agent;

        /// <summary>
        /// Exit point of the room
        /// </summary>
        private Vector3 _exitPoint;

        /// <summary>
        /// Time left for the monster before he has to leave
        /// </summary>
        private float _timer;

        /// <summary>
        /// Is the monster trying to leave the room
        /// </summary>
        public bool IsLeaving { private set; get; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.SetDestination(Vector3.zero);

            _timer = 6f;
            _exitPoint = transform.position;
        }

        private void Update()
        {
            if (!IsLeaving)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    IsLeaving = true;
                    _agent.SetDestination(_exitPoint);
                }
            }
        }
    }
}
