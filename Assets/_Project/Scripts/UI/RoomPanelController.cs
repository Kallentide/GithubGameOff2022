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
        private List<Button> _validButtons = new ();
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
                bool validRoom = monster.Needs.ContainsKey(childrenButton.gameObject.GetComponent<RoomPanelButtonController>().RoomNeed);
                if (validRoom)
                {
                    _validButtons.Add(childrenButton);
                }
                childrenButton.gameObject.SetActive(validRoom);
            }
            var selected = _validButtons[_selectedButtonIndex];
            selected.Select();
            _selectedButton = selected;
            _anim.SetBool("OpenRoomWheel", true);
        }

        public void ClosePanel()
        {
            _anim.SetBool("OpenRoomWheel", false);
            IsOpen = false;
            _selectedButtonIndex = 0;
            _validButtons = new List<Button>();
            _monster.IsInterracting = false;
            _monster = null;
        }

        public void Navigate(Vector2 dir)
        {
            if (dir.y == 1f) // up
            {
                _selectedButtonIndex--;
                if (_selectedButtonIndex == -1) _selectedButtonIndex = _validButtons.Count - 1;
            }
            else if (dir.y == -1f) // down
            {
                _selectedButtonIndex++;
                if (_selectedButtonIndex == _validButtons.Count) _selectedButtonIndex = 0;
            }
            var btn = _validButtons.ElementAt(_selectedButtonIndex);
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

        private void DisableChildrenButtons()
        {
            foreach (Button childrenButton in _childrenButtons)
            {
                childrenButton.gameObject.SetActive(false);
            }
        }
    }
}
