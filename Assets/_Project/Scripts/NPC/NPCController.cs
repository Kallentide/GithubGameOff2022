using GithubGameOff2022.Prop;
using UnityEngine;
using UnityEngine.AI;
using GithubGameOff2022.SO;
using System.Collections.Generic;
using System.Linq;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour
    {
        [SerializeField] private MonsterInfo _monsterSO;
        [SerializeField] private Vector3 _waitPoint;

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
        /// List of needs ordered
        /// </summary>
        private Dictionary<Need, int> _needs;

        /// <summary>
        /// Is the monster trying to leave the room
        /// </summary>
        public bool IsLeaving { private set; get; }

        private Chair _targetChair;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _needs = _monsterSO.Needs.ToDictionary(x => x, _ => Random.Range(30, 70));
            _timer = 25f;
            _exitPoint = transform.position;

            _agent.SetDestination(_waitPoint);
        }

        private void Update()
        {
            if (!IsLeaving)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    Leave();
                }
            }
        }

        private void Leave()
        {
            if (_targetChair != null)
            {
                // Free the chair
                _targetChair.IsBusy = false;
                _targetChair = null;
            }

            IsLeaving = true;
            _agent.SetDestination(_exitPoint);
        }

        public void SatisfyNeed(Need need, int amount)
        {
            if (_needs.ContainsKey(need))
            {
                var currValue = _needs[need] - amount;
                if (currValue <= 0)
                {
                    _needs.Remove(need);
                    if (!_needs.Any())
                    {
                        Leave();
                    }
                }
                else
                {
                    _needs[need] = currValue;
                }
            }
        }
    }
}
