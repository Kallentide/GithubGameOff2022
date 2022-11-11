﻿using GithubGameOff2022.Prop;
using GithubGameOff2022.Player;
using GithubGameOff2022.SO;
using GithubGameOff2022.Translation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace GithubGameOff2022.NPC
{
    public class NPCController : MonoBehaviour, IInteractible
    {
        [SerializeField] private Vector3 _waitPoint;

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
        public Dictionary<Need, int> Needs;

        /// <summary>
        /// Is the monster trying to leave the room
        /// </summary>
        public bool IsLeaving { private set; get; }
        public bool IsInterracting { set; private get; }

        private FulfillmentSlot _fulfillmentSlot;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _timer = 25f;
            _exitPoint = transform.position;

            _agent.SetDestination(_waitPoint);
        }

        private void Start()
        {
            IsInterracting = false;
            Needs = MonsterSO.Needs.ToDictionary(x => x, _ => Random.Range(30, 70));
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
            if (_fulfillmentSlot != null)
            {
                _fulfillmentSlot.IsFree = true;
                _fulfillmentSlot = null;
            }

            IsLeaving = true;
            _agent.SetDestination(_exitPoint);
        }

        public void SatisfyNeed(Need need, int amount)
        {
            if (!Needs.ContainsKey(need))
            {
                return;
            }
            Needs[need] -= amount;

            if (Needs.Sum(x => x.Value) == 0)
            {
                Leave();
            }
        }

        public void TryTakeFulfillmentSlot(Need need)
        {
            // Assign this monster to a free slot, then move to it 
            _fulfillmentSlot = RoomManager.Instance.GetFreeSlot(need);
            if (_fulfillmentSlot == null) return;
            
            _fulfillmentSlot.IsFree = false;
            _agent.SetDestination(_fulfillmentSlot.transform.position);
        }

        public void DoAction(PlayerController player)
        {
            if (player.Hands == null)
            {
                IsInterracting = true;
                player.OpenRoomPanel(this);
            }
            else
            {
                SatisfyNeed(player.Hands.Item.TargetNeed, 100);
                player.Hands = null;
            }
        }

        public bool CanInterract(PlayerController player)
        {
            return (!IsInterracting) && 
                (player.Hands == null || MonsterSO.Needs.Contains(player.Hands.Item.TargetNeed));
        }

        public string GetInteractionName(PlayerController player)
        {
            return CanInterract(player) ? Translate.Instance.Tr("interactNPC") : string.Empty;
        }
    }
}
