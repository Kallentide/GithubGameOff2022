using GithubGameOff2022.Player;

namespace GithubGameOff2022.Prop
{
    public interface IInteractible
    {
        public void DoAction(PlayerController player);

        public bool CanInterract(PlayerController player);

        public string GetInteractionName(PlayerController player);
    }
}
