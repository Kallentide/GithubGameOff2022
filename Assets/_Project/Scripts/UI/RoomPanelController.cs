using GithubGameOff2022.NPC;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GithubGameOff2022.UI
{
    public class RoomPanelController : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        private Button[] _childrenButtons;
        private List<Button> _interactableButtons = new ();
        private Button _selectedButton;
        private int _selectedButtonIndex;
        [HideInInspector] public bool IsOpen;
        private NPCController _monster;

        void Start()
        {
            IsOpen = false;
            _selectedButtonIndex = 0;
            _childrenButtons = GetComponentsInChildren<Button>();
        }

        public void OpenPanel(NPCController monster)
        {
            _monster = monster;
            IsOpen = true;
            
            foreach (Button childrenButton in _childrenButtons)
            {
                // Determines active buttons if monster need them, and wich are interactable
                // based on their fulfillement, but let active buttons displayed
                var buttonNeed = childrenButton.gameObject.GetComponent<RoomPanelButtonController>().RoomNeed;
                bool isActive = monster.Needs.ContainsKey(buttonNeed);
                bool isInteractable = false;

                if (isActive)
                {
                    isInteractable = monster.Needs[buttonNeed] > 0;
                    if (isInteractable)
                    {
                        _interactableButtons.Add(childrenButton);
                    }
                }
                childrenButton.gameObject.SetActive(isActive);
                childrenButton.interactable = isInteractable;
            }
            var selected = _interactableButtons[_selectedButtonIndex];
            selected.Select();
            _selectedButton = selected;
            _anim.SetBool("OpenRoomWheel", true);
        }

        public void ClosePanel()
        {
            _anim.SetBool("OpenRoomWheel", false);
            IsOpen = false;
            _selectedButtonIndex = 0;
            _interactableButtons = new List<Button>();
            _monster.IsPlayerInterracting = false;
            _monster = null;
        }

        public void Navigate(Vector2 dir)
        {
            if (dir.y == 1f) // up
            {
                _selectedButtonIndex--;
                if (_selectedButtonIndex == -1) _selectedButtonIndex = _interactableButtons.Count - 1;
            }
            else if (dir.y == -1f) // down
            {
                _selectedButtonIndex++;
                if (_selectedButtonIndex == _interactableButtons.Count) _selectedButtonIndex = 0;
            }
            var btn = _interactableButtons.ElementAt(_selectedButtonIndex);
            _selectedButton = btn;
            btn.Select();
        }

        public void Cancel()
        {
            ClosePanel();
        }

        public void Click()
        {
            if (!_monster.IsLeaving)
            {
                _monster.TryTakeFulfillmentSlot(_selectedButton.gameObject.GetComponent<RoomPanelButtonController>().RoomNeed);
            }
            ClosePanel();
        }
    }
}
