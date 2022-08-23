using UnityEditor.Animations;
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

        [SerializeField, Tooltip("The animator controller asset that controls this candle's animations.")]
        private AnimatorController _animationController;

        /// <summary>
        /// The intial length of the candle.
        /// </summary>
        public float Length { get => _length; }

        /// <summary>
        /// The last possible index of the candle's state.
        /// </summary>
        public int MaxStateIndex { get => _numberOfCandleStates - 1; }

        /// <summary>
        /// The factorial the candle length is divided by for the candleState"/>
        /// </summary>
        public float DivisibleFactor { get => Length / MaxStateIndex; }

        /// <summary>
        /// The initial candle color.
        /// </summary>
        public CandleColor Color { get => _color; }

        public AnimatorController AnimationController { get => _animationController; }
    }
}
