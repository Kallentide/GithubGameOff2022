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
        private Image[] _childrenButtons;
        private List<Image> _interactableButtons = new ();
        private Image _selectedButton;
        private int _selectedButtonIndex;
        [HideInInspector] public bool IsOpen;
        private NPCController _monster;

        private Color _baseColor, _selectedColor;

        void Start()
        {
            IsOpen = false;
            _selectedButtonIndex = 0;
            _childrenButtons = new Image[transform.childCount];
            for (var i = 0; i < transform.childCount; i++)
            {
                _childrenButtons[i] = transform.GetChild(i).GetComponent<Image>();
            }
            _baseColor = _childrenButtons[0].color;
            _selectedColor = new Color(_baseColor.r + .2f, _baseColor.g + .2f, _baseColor.b + .2f);
        }

        public void OpenPanel(NPCController monster)
        {
            _monster = monster;
            IsOpen = true;
            
            foreach (var child in _childrenButtons)
            {
                // Determines active buttons if monster need them, and wich are interactable
                // based on their fulfillement, but let active buttons displayed
                var buttonNeed = child.gameObject.GetComponent<RoomPanelButtonController>().RoomNeed;
                bool isActive = buttonNeed == Need.None || monster.Needs.ContainsKey(buttonNeed);
                bool isInteractable = false;

                if (isActive)
                {
                    isInteractable = buttonNeed == Need.None || monster.Needs[buttonNeed] > 0;
                    if (isInteractable)
                    {
                        _interactableButtons.Add(child);
                    }
                }
                child.gameObject.SetActive(isActive);
                child.color = isInteractable ? _baseColor : Color.gray;
            }
            var selected = _interactableButtons[_selectedButtonIndex];
            Select(selected);
            _anim.SetBool("OpenRoomWheel", true);
        }

        public void ClosePanel()
        {
            _anim.SetBool("OpenRoomWheel", false);
            IsOpen = false;
            _selectedButtonIndex = 0;
            _interactableButtons = new();
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
            Select(btn);
        }

        private void Select(Image img)
        {
            if (_selectedButton != null)
            {
                _selectedButton.color = _baseColor;
            }
            _selectedButton = img;
            _selectedButton.color = _selectedColor;
        }

        public void Cancel()
        {
            ClosePanel();
        }

        public void Click()
        {
            var targetNeed = _selectedButton.gameObject.GetComponent<RoomPanelButtonController>().RoomNeed;
            if (targetNeed != Need.None && !_monster.IsLeaving)
            {
                _monster.TryTakeFulfillmentSlot(targetNeed);
            }
            ClosePanel();
        }
    }
}
