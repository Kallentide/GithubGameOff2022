using UnityEngine;

namespace GithubGameOff2022
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { private set; get; }

        [SerializeField]
        private Camera _officeCamera;

        private void Awake()
        {
            Instance = this;
        }

        private GameObject[] _players;

        public void EnableOfficeView()
        {
            _officeCamera.gameObject.SetActive(true);
            _players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var p in _players)
            {
                p.gameObject.SetActive(false);
            }
        }

        public void EnableGameView()
        {
            _officeCamera.gameObject.SetActive(false);
            foreach (var p in _players)
            {
                p.gameObject.SetActive(true);
            }
        }
    }
}
