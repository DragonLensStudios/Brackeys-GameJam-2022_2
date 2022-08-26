using UnityEngine;
using UnityEngine.InputSystem;

namespace DragonLens.BrackeysGameJam2022_2.Dialogue
{
    public class DialogueInput : MonoBehaviour
    {
        private DialogueController _dialogueController;
        private PlayerInputActions _inputActions;
        private InputAction _nextMessage;

        private void Awake() {
            _dialogueController = GetComponent<DialogueController>();
            if(_dialogueController == null) {
                Debug.LogError($"{typeof(DialogueInput)} could get find a reference to {typeof(DialogueController)}", this);
            }

            _inputActions = new PlayerInputActions();
            _nextMessage = _inputActions.Player.Activate;
        }

        private void OnEnable() {
            if(_dialogueController == null) return;

            _dialogueController.OnDialogueStart += EnableInput;
            _dialogueController.OnDialogueEnd += DisableInput;
        }

        private void OnDisable() {
            if(_dialogueController == null) return;

            _dialogueController.OnDialogueStart -= EnableInput;
            _dialogueController.OnDialogueEnd -= DisableInput;
        }

        private void EnableInput() {
            _nextMessage.performed += NextMessage;
            _nextMessage.Enable();
        }

        private void DisableInput() {
            _nextMessage.Disable();
            _nextMessage.performed -= NextMessage;
        }

        private void NextMessage(InputAction.CallbackContext input) {
            if(_dialogueController == null) return;

            _dialogueController.DisplayNextMessage();
        }
    }
}
