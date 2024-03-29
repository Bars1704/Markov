using System;
using Markov.MarkovTest.Misc;
using Markov.MarkovTest.Sequences;
using Markov.MarkovTest.TwoDimension.Patterns;
using Newtonsoft.Json;

namespace Markov.MarkovTest.TwoDimension.Rules
{
    public abstract class RuleBase<T> : ISequencePlayable<T, MarkovSimulation<T>>, IResizable where T : IEquatable<T>
    {
        [JsonProperty] public T[,] Stamp { get; set; }

        [JsonProperty] public Pattern<T> MainPattern { get; private set; }
        public Vector2Int Size => MainPattern.Size;

        protected RuleBase(Pattern<T> mainPattern, T[,] stamp)
        {
            Stamp = stamp;
            MainPattern = mainPattern;
        }

        protected RuleBase()
        {
            MainPattern = new Pattern<T>();
            Stamp = new T[1, 1];
        }

        protected void ApplyRuleEvent(Vector2Int coord, PatternDeformation2D rotationSettings)
        {
            OnRuleApplied?.Invoke(coord, rotationSettings);
            OnPlayed?.Invoke();
        }

        public event Action<Vector2Int, PatternDeformation2D> OnRuleApplied;

        public event Action OnPlayed;
        public abstract void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric);

        public void Resize(Vector2Int newSize)
        {
            Stamp = MatrixFormatter<T>.Resize(Stamp, newSize.X, newSize.Y);
            MainPattern.Resize(newSize);
        }
    }
}