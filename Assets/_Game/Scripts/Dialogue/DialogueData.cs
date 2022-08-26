using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Dialogue
{
    [CreateAssetMenu(menuName = "Game Jam/Dialogue/Data")]
    public class DialogueData : ScriptableObject
    {
        [SerializeField]
        private DialogueMessage[] _messages;

        public DialogueMessage[] Messages => _messages;

        public bool RequestDialogueStart() {
            DialogueController controller = FindObjectOfType<DialogueController>();
            if (controller == null) {
                Debug.LogWarning("There is no dialogue controller in the scene. Request failed!", this);
                return false;
            }

            controller.StartDialogue(this);
            return true;
        }
    }

    [System.Serializable]
    public class DialogueMessage
    {
        [SerializeField]
        private DialogueActor _actor;

        [SerializeField, TextArea(5, 5)]
        private string _text;

        public string ActorName { get => _actor.Name; }
        public string Text { get => _text; }
    }
}
