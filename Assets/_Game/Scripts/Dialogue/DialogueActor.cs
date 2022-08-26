using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Dialogue
{
    [CreateAssetMenu(menuName = "Game Jam/Dialogue/Actor Data")]
    public class DialogueActor : ScriptableObject
    {
        [SerializeField]
        private string _name;

        public string Name => _name;
    }
}
