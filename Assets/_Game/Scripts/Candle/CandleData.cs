using UnityEngine;

namespace DragonLens.BrackeysGameJam2022_2.Candles
{
    [CreateAssetMenu(menuName = "Game Jam/Candle")]
    public class CandleData : ScriptableObject
    {
        [Header("Duration")]
        [SerializeField, Tooltip("Length in seconds the candle will last.")]
        private float _length = 60f;

        [SerializeField, Tooltip("Candle Length / this = Candle State| 12 being full. " +
            "The animation for the candle is 12 frames so 60 / 5 = 12 you want to match the candle length with a respective number here.")]
        private float _divisibleFactor = 5f;

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
        public float DivisibleFactor { get => _divisibleFactor; }

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
            CurrentState = (int)(Length / DivisibleFactor);
            CurrentColor = _color;
        }

        private void Reset() => Initialize();
    }
}
