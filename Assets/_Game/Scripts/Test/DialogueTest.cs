using DragonLens.BrackeysGameJam2022_2.Dialogue;
using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Tests
{
    public class DialogueTest : MonoBehaviour
    {
        public DialogueData data;

        public void StartDialogue() {
            data.RequestDialogueStart();
        }
    }
}
