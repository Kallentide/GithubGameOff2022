using GithubGameOff2022.NPC;
using GithubGameOff2022.Translation;
using TMPro;
using UnityEngine;

namespace GithubGameOff2022.UI
{
    public class RoomPanelButtonController : MonoBehaviour 
    {
        public Need RoomNeed;
        [SerializeField] private string _roomNameTransKey;
        [SerializeField] private TextMeshProUGUI _roomText;

        void Start()
        {
            _roomText.text = Translate.Instance.Tr(_roomNameTransKey);
            if (RoomNeed == Need.None)
            {
                Debug.LogWarning("This RoomPanelButtonController can't have the None need selected");
            }
        }
    }   
}
