using GithubGameOff2022.NPC;
using GithubGameOff2022.Player;
using UnityEngine;

namespace GithubGameOff2022.Prop.NeedSlot
{
    public abstract class AFulfillmentSlot : MonoBehaviour, IInteractible
    {
        public abstract Need FulfilledNeed { get; }
        public bool IsFree { set; get; }

        public abstract bool CanInterract(PlayerController player);

        public abstract void DoAction(PlayerController player);

        public abstract string GetInteractionName(PlayerController player);

        private void Start()
        {
            IsFree = true;
            RoomManager.Instance.RegisterSlot(this, FulfilledNeed);
        }
    }
}
