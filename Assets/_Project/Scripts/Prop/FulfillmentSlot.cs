using GithubGameOff2022.NPC;
using UnityEngine;

namespace GithubGameOff2022.Prop
{
    public class FulfillmentSlot : MonoBehaviour
    {
        [SerializeField] private Need _fulfilledNeed;
        public bool IsFree { set; get; }

        private void Start()
        {
            IsFree = true;
            RoomManager.Instance.RegisterSlot(this, _fulfilledNeed);
        }
    }
}
