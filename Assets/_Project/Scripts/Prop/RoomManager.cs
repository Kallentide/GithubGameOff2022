using GithubGameOff2022.NPC;
using GithubGameOff2022.Prop.NeedSlot;
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

        private readonly Dictionary<Need, List<AFulfillmentSlot>> _slots = new();

        public void RegisterSlot(AFulfillmentSlot slot, Need need)
        {
            if (!_slots.ContainsKey(need))
            {
                _slots.Add(need, new());
            }
            _slots[need].Add(slot);
        }

        public AFulfillmentSlot GetFreeSlot(Need need)
        {
            var freeSlots = GetNeedSlots(need).Where(x => x.IsFree).ToArray();
            if (!freeSlots.Any())
            {
                return null;
            }
            return freeSlots[Random.Range(0, freeSlots.Length)];
        }

        private IEnumerable<AFulfillmentSlot> GetNeedSlots(Need need)
        {
            // Associate a need with a list of slots
            return _slots.FirstOrDefault(x => x.Key == need).Value;
        }
    }
}
