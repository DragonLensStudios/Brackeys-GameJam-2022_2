using DragonLens.BrackeysGameJam2022_2.Dialogue;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Tests
{
    public class DialogueTest : MonoBehaviour
    {
        [SerializeField]
        private bool _startDialogue;

        [SerializeField]
        private DialogueData _dialogueData;

        private void Update() {
            if(_startDialogue) {
                _startDialogue = false;
                StartDialogue();
            }
        }

        private void StartDialogue() {
            _dialogueData.RequestDialogueStart();
        }
    }
}
