using GithubGameOff2022.NPC;
using GithubGameOff2022.Player;
using UnityEngine;

namespace GithubGameOff2022.Prop.NeedSlot
{
    public class DefaultSlot : AFulfillmentSlot
    {
        [SerializeField]
        private Need _need;

        public override Need FulfilledNeed => _need;

        public override bool CanInterract(PlayerController player) => false;

        public override void DoAction(PlayerController player)
        { }

        public override string GetInteractionName(PlayerController player) => string.Empty;
    }
}
