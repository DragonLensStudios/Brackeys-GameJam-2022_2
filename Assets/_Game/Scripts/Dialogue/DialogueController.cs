using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Dialogue
{
	public class DialogueController : MonoBehaviour
	{
        public bool isDebugMode;
        public TextMeshProUGUI dialogueText;
        public TextMeshProUGUI dialogueActor;

        public Animator animator;
        private bool _isAnimating;

        private Queue<DialogueMessage> _messages;
        private DialogueMessage _currentMessage;

        private void Awake()
        {
            _messages = new();
        }

        public void StartDialogue(DialogueData dialogue)
        {
            animator.SetBool("IsOpen", true);

            _messages.Clear();

            foreach(DialogueMessage message in dialogue.Messages)
            {
                _messages.Enqueue(message);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if(_isAnimating)
            {
                if(isDebugMode)
                {
                    StopAllCoroutines();
                    _isAnimating = false;
                    dialogueText.text = _currentMessage.Text;
                    dialogueActor.text = _currentMessage.ActorName;
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
            StartCoroutine(AnimateSentence(_currentMessage));
        }

        IEnumerator AnimateSentence(DialogueMessage message)
        {
            _isAnimating = true;
            dialogueText.text = "";
            dialogueActor.text = message.ActorName;
            yield return new WaitForSeconds(0.5f);

            foreach(char letter in message.Text.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }

            _isAnimating = false;
        }

        void EndDialogue()
        {
            animator.SetBool("IsOpen", false);
            // TODO: Notify listeners with a dialogue end event?
        }
    }
}
