using GithubGameOff2022.Player;
using GithubGameOff2022.Translation;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GithubGameOff2022
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { private set; get; }

        [SerializeField]
        private TMP_Text _readyText;

        public UnityEvent OnReady { private set; get; } = new();

        private void Awake()
        {
            Instance = this;
            UpdateReadyText();
        }

        /// <summary>
        /// Check if all players are ready, if so, start the game, also update the UI
        /// </summary>
        public void CheckAllPlayerReady()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            // We check if there are players because this method is called when player join and leave
            if (players.Any() && players.All(x => x.GetComponent<PlayerController>().IsReady))
            {
                OnReady.Invoke();
                _readyText.gameObject.SetActive(false);
            }
            else
            {
                UpdateReadyText();
            }
        }

        public void UpdateReadyText()
        {
            var playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
            if (playerCount == 0)
            {
                playerCount = 1; // No point displaying there is 0 player since we can't play that way anyway
            }
            _readyText.text = Translate.Instance.Tr("ready") + $" {GameObject.FindGameObjectsWithTag("Player").Count(x => x.GetComponent<PlayerController>().IsReady)}/{playerCount}";
        }

        public void OnPlayerCountChange(PlayerInput _)
        {
            CheckAllPlayerReady();
        }
    }
}
