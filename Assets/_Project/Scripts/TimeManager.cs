using GithubGameOff2022.Player;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GithubGameOff2022
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { private set; get; }

        public UnityEvent OnReady { private set; get; }

        private void Awake()
        {
            Instance = this;
            OnReady = new();
        }

        public bool AreAllPlayerReady()
        {
            return GameObject.FindGameObjectsWithTag("Player").All(x => x.GetComponent<PlayerController>().IsReady);
        }
    }
}
