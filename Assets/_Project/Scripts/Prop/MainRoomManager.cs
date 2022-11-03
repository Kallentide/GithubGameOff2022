using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GithubGameOff2022.Prop
{
    public class MainRoomManager : MonoBehaviour
    {
        public static MainRoomManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private readonly List<Chair> _chairs = new();

        public void RegisterChair(Chair chair)
        {
            _chairs.Add(chair);
        }

        public Chair FreeChair
        {
            get
            {
                var freeChairs = _chairs.Where(x => !x.IsBusy).ToArray();
                if (!freeChairs.Any())
                {
                    return null;
                }
                return freeChairs[Random.Range(0, freeChairs.Length)];
            }
        }
    }
}
