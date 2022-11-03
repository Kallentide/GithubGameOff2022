using UnityEngine;

namespace GithubGameOff2022.Prop
{
    public class Chair : MonoBehaviour
    {
        public bool IsBusy { set; get; }

        private void Start()
        {
            MainRoomManager.Instance.RegisterChair(this);
        }
    }
}
