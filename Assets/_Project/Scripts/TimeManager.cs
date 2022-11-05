using DG.Tweening;
using GithubGameOff2022.NPC;
using GithubGameOff2022.Player;
using GithubGameOff2022.SO;
using GithubGameOff2022.Translation;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GithubGameOff2022
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { private set; get; }

        [SerializeField]
        private TMP_Text _readyText;

        [SerializeField]
        private Image _dayProgress;

        public UnityEvent OnReady { private set; get; } = new();
        private Tween _tween;

        [SerializeField]
        private RoundInfo _info;

        public bool DidDayStart { private set; get; }

        private void Awake()
        {
            Instance = this;
            OnReady.AddListener(new(() =>
            {
                _readyText.gameObject.SetActive(false);
                _dayProgress.gameObject.SetActive(true);
                _dayProgress.fillAmount = 0f;
                DidDayStart = true;
                _tween = DOTween.To(() => _dayProgress.fillAmount,
                    setter: x => _dayProgress.fillAmount = x, 1f, _info.RoundDuration)
                    .OnUpdate(
                        () =>
                        {}).OnComplete(() =>
                        {
                            _readyText.gameObject.SetActive(true);
                            _dayProgress.gameObject.SetActive(false);
                            DidDayStart = false;
                            ResetReady();
                        }).SetUpdate(true);
            }));
            UpdateReadyText();
        }

        public void ResetReady() // TODO: Keep objects in list instead of searching them on scene everytimes
        {
            foreach (var m in GameObject.FindGameObjectsWithTag("Monster"))
            {
                m.GetComponent<NPCController>().Leave();
            }
            foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
            {
                p.GetComponent<PlayerController>().IsReady = false;
            }
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
