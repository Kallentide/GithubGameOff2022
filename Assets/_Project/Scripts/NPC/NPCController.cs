using GithubGameOff2022.Prop;
using UnityEngine;
using UnityEngine.AI;
using GithubGameOff2022.SO;
using System.Collections.Generic;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour
    {
        public enum Need {
            Bath = 1, 
            Massage = 2,
            Drink = 3
        };

        [SerializeField]
        private MonsterInfo _monsterSO;

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
        /// Queue of needs ordered
        /// </summary>
        private Queue<Need> _needs;

        /// <summary>
        /// Current need 
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

        private Chair _targetChair;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _needs = new Queue<Need>(_monsterSO.Needs);
            _currentNeed = _needs.Dequeue();
            _timer = 9f;
            _exitPoint = transform.position;
 
            UpdateDestination();
       }

        private void Update()
        {
            if (!IsLeaving)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    if (_targetChair != null)
                    {
                        // Free the chair
                        _targetChair.IsBusy = false;
                        _targetChair = null;
                    }
                    IsLeaving = true;
                    UpdateDestination();
                }
                else if (_currentNeedPercentage >= 100)
                {
                    _currentNeedPercentage = 0;

                    // Currend need full, take the next it if any
                    if (_needs.Count > 0)
                    {
                        _currentNeed = _needs.Dequeue();
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
            else if (_currentNeed == Need.Drink)
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
            else if (_currentNeed == Need.Drink)
            {
                // We get a random chair and say its ours, then move to it
                _targetChair = MainRoomManager.Instance.FreeChair;
                _targetChair.IsBusy = true;
                _agent.SetDestination(_targetChair.transform.position);
            }
        }
    }
}
