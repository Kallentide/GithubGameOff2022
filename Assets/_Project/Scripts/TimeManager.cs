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
    }
}
