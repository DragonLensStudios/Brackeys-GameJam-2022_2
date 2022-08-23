using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Candles
{
    [CreateAssetMenu(menuName = "Game Jam/Candle")]
    public class CandleData : ScriptableObject
    {
        [Header("Duration")]
        [SerializeField, Tooltip("Length in seconds the candle will last.")]
        private float _length = 60f;

        [SerializeField, Tooltip("How many candle states are there?")]
        private int _numberOfCandleStates = 13;

        [Header("Color")]
        [SerializeField, Tooltip("The starting color of the candle.")]
        private CandleColor _color = CandleColor.Yellow;

        /// <summary>
        /// The intial length of the candle.
        /// </summary>
        public float Length { get => _length; }

        /// <summary>
        /// The factorial the candle length is divided by for the candleState"/>
        /// </summary>
        public float DivisibleFactor { get; private set; }

        /// <summary>
        /// The initial candle color.
        /// </summary>
        public CandleColor Color { get => _color; }

        /// <summary>
        /// The current candle color.
        /// </summary>
        public CandleColor CurrentColor { get; set; }

        /// <summary>
        /// The current state of candle. 12 = new, 0 = out.
        /// </summary>
        public int CurrentState { get; set; }

        public void Initialize() {
            int startingStateIndex = _numberOfCandleStates - 1;
            DivisibleFactor = Length / startingStateIndex;
            CurrentState = startingStateIndex;
            CurrentColor = _color;
        }

        private void Reset() => Initialize();

#if UNITY_EDITOR
        private void OnValidate() {
            Initialize();
        }
#endif
    }
}
