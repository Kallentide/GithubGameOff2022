using GithubGameOff2022.Prop;
using GithubGameOff2022.SO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GithubGameOff2022.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo _info;

        private CharacterController _cc;
        private Vector3 _mov;
        private float _verSpeed;

        private TMP_Text _indicatorText;
        private IInteractible _interactionTarget;

        public bool IsReady { set; get; } // Used at the start of a day, game only starts when all people are ready

        private void Awake()
        {
            _cc = GetComponent<CharacterController>();

            var triggerZone = GetComponentInChildren<TriggerDetector>();
            triggerZone.TriggerEnterEvent.AddListener(new((coll) =>
            {
                var target = coll.GetComponent<IInteractible>();
                if (target != null && target.CanInterract(this))
                {
                    _indicatorText.text = target.GetInteractionName(this);
                    _interactionTarget = target;
                }
            }));
            triggerZone.TriggerExitEvent.AddListener(new((coll) => {
                _interactionTarget = null;
                _indicatorText.text = string.Empty;
            }));
        }

        private void FixedUpdate()
        {
            Vector3 desiredMove = new(_mov.x, 0f, _mov.y);

            // Get a normal for the surface that is being touched to move along it
            Physics.SphereCast(transform.position, _cc.radius, Vector3.down, out RaycastHit hitInfo,
                               _cc.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            Vector3 moveDir = Vector3.zero;
            moveDir.x = desiredMove.x * _info.Speed;
            moveDir.z = desiredMove.z * _info.Speed;

            if (_cc.isGrounded && _verSpeed < 0f) // We are on the ground and not jumping
            {
                moveDir.y = -.1f; // Stick to the ground
                _verSpeed = -_info.GravityMultiplicator;
            }
            else
            {
                // We are currently jumping, reduce our jump velocity by gravity and apply it
                _verSpeed += Physics.gravity.y * _info.GravityMultiplicator;
                moveDir.y += _verSpeed;
            }

            _cc.Move(moveDir);
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            _mov = value.ReadValue<Vector2>().normalized;
        }

        public void OnAction(InputAction.CallbackContext value)
        {
            if (value.performed && _interactionTarget != null)
            {
                if (_interactionTarget.CanInterract(this)) // Just to be sure a player didn't snipe 
                {
                    _interactionTarget.DoAction(this);
                    if (!_interactionTarget.CanInterract(this)) // Can we interact with the object again?
                    {
                        _interactionTarget = null;
                        _indicatorText.text = string.Empty;
                    }
                }
            }
        }
    }
}
