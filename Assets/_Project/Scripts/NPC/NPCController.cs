using GithubGameOff2022.Prop;
using UnityEngine;
using UnityEngine.AI;
using GithubGameOff2022.SO;
using System.Collections.Generic;
using System.Linq;
using GithubGameOff2022.Player;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour, IInteractible
    {
        public MonsterInfo MonsterSO { set; private get; }

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
            _timer = 9f;
            _exitPoint = transform.position;

            // We get a random chair and say its ours, then move to it
            _targetChair = MainRoomManager.Instance.FreeChair;
            _targetChair.IsBusy = true;
            _agent.SetDestination(_targetChair.transform.position);
        }

        private void Start()
        {
            _needs = MonsterSO.Needs.ToDictionary(x => x, _ => Random.Range(30, 70));
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

        public void Leave()
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

        public void DoAction(PlayerController player)
        {
            if (player.Hands == null)
            {
                // TODO: Speak with monsters
            }
            else
            {
                SatisfyNeed(player.Hands.Item.TargetNeed, 100);
                player.Hands = null;
            }
        }

        public bool CanInterract(PlayerController player)
        {
            return player.Hands == null || MonsterSO.Needs.Contains(player.Hands.Item.TargetNeed);
        }

        public string GetInteractionName(PlayerController player)
        {
            return CanInterract(player) ? "TODO" : string.Empty;
        }
    }
}
