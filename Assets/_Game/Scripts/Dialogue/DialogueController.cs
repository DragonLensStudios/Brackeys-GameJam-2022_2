using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Dialogue
{
	public class DialogueController : MonoBehaviour
	{
        [SerializeField, Tooltip("Can the text animation be skipped?")]
        private bool _canSkipAnimation;
        [SerializeField, Tooltip("How long should the text animation last?")]
        private float _animationDuration = 1f;
        [SerializeField, Tooltip("How long should we wait to start the text animation?")]
        private float _animationStartDelay = 0.5f;

        [Header("TMP Text")]
        [SerializeField]
        private TextMeshProUGUI _dialogueActor;
        [SerializeField]
        private TextMeshProUGUI _dialogueMessage;

        private Animator _animator;
        private bool _isAnimating;
        private Queue<DialogueMessage> _messages;
        private DialogueMessage _currentMessage;

        public event Action OnDialogueStart;
        public event Action OnDialogueEnd;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            if(_animator == null) {
                Debug.LogError($"{typeof(DialogueController)} could not find an animator component.", this);
            }

            _messages = new();
        }

        public void StartDialogue(DialogueData dialogue)
        {
            _animator.SetBool("IsOpen", true);

            _messages.Clear();

            foreach(DialogueMessage message in dialogue.Messages)
            {
                _messages.Enqueue(message);
            }

            DisplayNextMessage();
            OnDialogueStart?.Invoke();
        }

        public void DisplayNextMessage()
        {
            if(_isAnimating)
            {
                if(_canSkipAnimation)
                {
                    StopAllCoroutines();
                    _isAnimating = false;
                    _dialogueMessage.text = _currentMessage.Text;
                    _dialogueActor.text = _currentMessage.ActorName;
                }
                return;
            }
            else if(_messages.Count == 0)
            {
                EndDialogue();
                return;
            }

            _currentMessage = _messages.Dequeue();
            StopAllCoroutines();
            StartCoroutine(AnimateMessage(_currentMessage));
        }

        private IEnumerator AnimateMessage(DialogueMessage message)
        {
            _isAnimating = true;
            _dialogueMessage.text = "";
            _dialogueActor.text = message.ActorName;
            char[] messageChars = message.Text.ToCharArray();
            float waitDuration = _animationDuration / messageChars.Length;
            yield return new WaitForSeconds(_animationStartDelay);

            foreach(char letter in messageChars)
            {
                _dialogueMessage.text += letter;
                yield return new WaitForSeconds(waitDuration);
            }

            _isAnimating = false;
        }

        private void EndDialogue()
        {
            _animator.SetBool("IsOpen", false);
            OnDialogueEnd?.Invoke();
        }
    }
}
