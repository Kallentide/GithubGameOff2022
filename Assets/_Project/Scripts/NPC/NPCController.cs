using GithubGameOff2022.Prop;
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

        private Chair _targetChair;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            // We get a random chair and say its ours, then move to it
            _targetChair = MainRoomManager.Instance.FreeChair;
            _targetChair.IsBusy = true;
            _agent.SetDestination(_targetChair.transform.position);

            _timer = 7f;
            _exitPoint = transform.position;
        }

        private void Update()
        {
            if (!IsLeaving)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    // We free the chair and go back to the exit
                    _targetChair.IsBusy = false;
                    _targetChair = null;
                    IsLeaving = true;
                    _agent.SetDestination(_exitPoint);
                }
            }
        }
    }
}
