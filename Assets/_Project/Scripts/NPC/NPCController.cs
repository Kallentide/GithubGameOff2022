using UnityEngine;
using UnityEngine.AI;
using GithubGameOff2022.SO;
using System.Collections.Generic;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField]
        private MonsterInfo _info;

        private NavMeshAgent _agent;

        /// <summary>
        /// Exit point of the room
        /// </summary>
        private Vector3 _exitPoint;

        /// <summary>
        /// Time left for the monster before he has to leave
        /// </summary>
        private float _timer;

        public enum Need {
            Bath = 1, 
            Massage = 2
        };

        /// <summary>
        /// Queue of needs ordered
        /// </summary>
        private Queue<Need> _needs;

        /// <summary>
        /// Current need enum
        /// </summary>
        private Need? _currentNeed;
        
        /// <summary>
        /// Current need amount from 0 to 100
        /// </summary>
        private float _currentNeedPercentage = 0;

        /// <summary>
        /// Is the monster trying to leave the room
        /// </summary>
        public bool IsLeaving { private set; get; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _needs = new Queue<Need>(_info.Needs);

            _currentNeed = _needs.Dequeue();
            Debug.Log("Current Need: " + _currentNeed);
            UpdateDestination();
            
            _timer = 9f;
            _exitPoint = transform.position;
        }

        private void Update()
        {
            if (!IsLeaving)
            {

                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    Debug.Log("Time out ! Leaving... (with current need " + _currentNeed + " at " + _currentNeedPercentage + "%)");
                    IsLeaving = true;
                    UpdateDestination();
                }
                else if (_currentNeedPercentage >= 100)
                {
                    Debug.Log("Need " + _currentNeed + " reached 100%");
                    _currentNeedPercentage = 0;

                    // Currend need full, take the next it if any
                    if (_needs.Count > 0)
                    {
                        _currentNeed = _needs.Dequeue();
                        Debug.Log("Current Need: " + _currentNeed);
                    }
                    else
                    {
                        IsLeaving = true;
                    }
                    UpdateDestination();
                }
                else
                {
                    UpdateNeedAmount();
                }
            }
        }

        private void UpdateNeedAmount()
        {
            // TODO: add conditions to increase need percentage fullfilment
            if (_currentNeed == Need.Bath)
            {
                _currentNeedPercentage += Time.deltaTime * 30;
            }
            else if (_currentNeed == Need.Massage)
            {
                _currentNeedPercentage += Time.deltaTime * 15;
            }
        }

        private void UpdateDestination()
        {
            // TODO: define real locations to each rooms
            if (IsLeaving || _currentNeed == null)
            {
                _agent.SetDestination(_exitPoint);
            }
            else if (_currentNeed == Need.Bath)
            {
                _agent.SetDestination(new Vector3(3,0,0));
            }
            else if (_currentNeed == Need.Massage)
            {
                _agent.SetDestination(new Vector3(-3,0,0));
            }
        }
    }
}
