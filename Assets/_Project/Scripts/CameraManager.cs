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

        public void EnableOfficeView()
        {
            _officeCamera.gameObject.SetActive(true);
            foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
            {
                p.gameObject.SetActive(false);
            }
        }

        public void EnableGameView()
        {
            _officeCamera.gameObject.SetActive(false);
            foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
            {
                p.gameObject.SetActive(true);
            }
        }
    }
}
