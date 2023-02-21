using MarkovTest.TwoDimension.Patterns;
using MarkovTest.TwoDimension.Sequences;
using Newtonsoft.Json;
using MarkovTest.Misc;

namespace MarkovTest.TwoDimension.Rules
{
    public abstract class RuleBase<T> : ISequencePlayable<T>, IResizable where T : IEquatable<T>
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
            Stamp = new T[0, 0];
        }

        protected void ApplyRuleEvent(Vector2Int coord, PatternDeformation rotationSettings)
        {
            OnRuleApplied?.Invoke(coord, rotationSettings);
            OnPlayed?.Invoke();
        }

        public event Action<Vector2Int, PatternDeformation> OnRuleApplied;

        public event Action? OnPlayed;
        public abstract void Play(MarkovSimulationTwoDim<T> simulation, RandomFabric randomFabric);

        public void Resize(Vector2Int newSize)
        {
            Stamp = MatrixFormatter<T>.Resize(Stamp, newSize.X, newSize.Y);
            MainPattern.Resize(newSize);
        }
    }
}