using GithubGameOff2022.NPC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GithubGameOff2022.Prop
{
    public class RoomManager : MonoBehaviour
    {
        public static RoomManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("There is more than one RoomManager instance in this scene");
            }
            Instance = this;
        }

        private readonly List<FulfillmentSlot> _bathSlots = new();
        private readonly List<FulfillmentSlot> _drinkSlots = new();

        public void RegisterSlot(FulfillmentSlot slot, Need need)
        {
            GetNeedSlots(need).Add(slot);
        }

        public FulfillmentSlot GetFreeSlot(Need need)
        {
            var freeSlots = GetNeedSlots(need).Where(x => x.IsFree).ToArray();
            if (!freeSlots.Any())
            {
                return null;
            }
            return freeSlots[Random.Range(0, freeSlots.Length)];
        }

        private List<FulfillmentSlot> GetNeedSlots(Need need)
        {
            // Associate a need with a list of slots
            switch (need)
            {
                case Need.Bath:
                    return _bathSlots;
                case Need.Drink:
                    return _drinkSlots;
                default:
                    Debug.LogWarning("Invalid need given");
                    return new List<FulfillmentSlot>();
            }
        }
    }
}
