using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Dialogue
{
    [CreateAssetMenu(menuName = "Game Jam/Dialogue/Data")]
    public class DialogueData : ScriptableObject
    {
        [SerializeField]
        private DialogueMessage[] _messages;

        public DialogueMessage[] Messages => _messages;
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
